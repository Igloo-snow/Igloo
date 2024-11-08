using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestStepUI : MonoBehaviour
{
    public Transform questListContainer;
    public GameObject questUIPrefab;
    public Button sceneTransitionButton;
    public string EndingScene;

    private QuestManager questManager;
    private readonly List<string> questIds = new List<string> { "DSQuest", "DDQuest", "LXQuest", "TypingQuest", "PLTQuest", "DiscreteMath", "AlgoQuest" };

    private void Start()
    {
        questManager = QuestManager.instance;
        sceneTransitionButton.gameObject.SetActive(false);
        sceneTransitionButton.onClick.AddListener(TransitionToScene);
        UpdateQuestCompletionUI();
    }

    public void UpdateQuestCompletionUI()
    {
        bool allQuestsCompleted = true;

        foreach (Transform child in questListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (string questId in questIds)
        {
            Quest quest = questManager.GetQuestById(questId);

            if (quest == null)
            {
                Debug.LogError("�ش� ID�� �ش��ϴ� ����Ʈ�� ã�� �� �����ϴ�: " + questId);
                continue;
            }

            GameObject questUIObj = Instantiate(questUIPrefab, questListContainer);

            TextMeshProUGUI questNameText = questUIObj.transform.Find("QuestName").GetComponent<TextMeshProUGUI>();
            if (questNameText != null)
            {
                questNameText.text = quest.info.displayName;
            }
            else
            {
                Debug.LogError("QuestName TextMeshProUGUI component not found.");
            }

            GameObject questIconObj = questUIObj.transform.Find("QuestIcon").gameObject;
            if (questIconObj != null)
            {
                questIconObj.SetActive(quest.state == QuestState.FINISHED);
            }
            else
            {
                Debug.LogError("QuestIcon object not found.");
            }

            if (quest.state != QuestState.FINISHED)
            {
                allQuestsCompleted = false;
            }
        }

        sceneTransitionButton.gameObject.SetActive(allQuestsCompleted);
    }

    public void TransitionToScene()
    {
        if (!string.IsNullOrEmpty(EndingScene))
        {
            SceneManager.LoadScene(EndingScene);
        }
        else
        {
            Debug.LogError("Ÿ�� �� �̸��� �������� �ʾҽ��ϴ�.");
        }
    }
}