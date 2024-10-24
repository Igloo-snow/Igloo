using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject[] quizUIPanels;
    public GameObject rewardUIPanel;
    public Button[] rewardButtons;
    public GameObject[] rewardItems;
    public AudioClip correct;
    public AudioClip wrong;
    private AudioSource audioSource;
    private List<int> remainingQuizzes;
    public int currentQuizIndex = -1;
    private bool isQuizActive = false;

    private GameObject player;
    private GameObject camera;
    private PlayerMovement playerMovement;

    public Transform dropPositionObject;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindWithTag("Player");
        camera = GameObject.Find("Camera");
        playerMovement = player.GetComponent<PlayerMovement>();

        remainingQuizzes = new List<int>();
        for (int i = 0; i < quizUIPanels.Length; i++)
        {
            remainingQuizzes.Add(i);
            quizUIPanels[i].SetActive(false);
        }

        rewardUIPanel.SetActive(false);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            int index = i;
            rewardButtons[i].onClick.AddListener(() => OnRewardSelected(index));
        }
    }

    void Update()
    {
        if (isQuizActive)
        {
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            camera.SetActive(false);
        }
        else
        {
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            camera.SetActive(true);
        }
    }

    public void StartQuiz()
    {
        if (remainingQuizzes.Count == 0)
        {
            ResetQuizList();
        }

        int randomIndex = Random.Range(0, remainingQuizzes.Count);
        currentQuizIndex = remainingQuizzes[randomIndex];
        remainingQuizzes.RemoveAt(randomIndex);

        quizUIPanels[currentQuizIndex].SetActive(true);
        isQuizActive = true;

        foreach (var panel in quizUIPanels)
        {
            var quizPanel = panel.GetComponent<QuizPanel>();
            if (quizPanel != null)
            {
                quizPanel.quizIndex = currentQuizIndex; // 올바른 인덱스를 설정
            }
        }
    }

    IEnumerator ShowRewardUI()
    {
        rewardUIPanel.SetActive(true);
        yield break;
    }

    public void OnRewardSelected(int itemIndex)
    {
        if (!isQuizActive) return;

        rewardUIPanel.SetActive(false);
        GiveItemToPlayer(rewardItems[itemIndex]);
        isQuizActive = false;
        currentQuizIndex = -1;
    }

    void GiveItemToPlayer(GameObject item)
    {
        if (dropPositionObject != null)
        {
            Vector3 dropPosition = dropPositionObject.position;
            GameObject droppedItem = Instantiate(item, dropPosition, Quaternion.identity);
            Debug.Log("아이템 지급: " + droppedItem.name);
        }
        else
        {
            Debug.LogError("Drop position object is not assigned!");
        }
    }

    void ResetQuizList()
    {
        remainingQuizzes.Clear();
        for (int i = 0; i < quizUIPanels.Length; i++)
        {
            remainingQuizzes.Add(i);
        }
    }

    public void OnAnswerSelected(bool isCorrect, int quizIndex)
    {
        if (isCorrect)
        {
            audioSource.PlayOneShot(correct);
            StartCoroutine(ShowRewardUI());
        }
        else
        {
            audioSource.PlayOneShot(wrong);
        }
        quizUIPanels[quizIndex].SetActive(false);
    }
}