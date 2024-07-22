using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Net.NetworkInformation;

public class AlgoSpeechBubbleManager : MonoBehaviour
{
    public static AlgoSpeechBubbleManager instance;

    [Header("Text")]
    [SerializeField] Transform textGroup;
    [SerializeField] AlgoSpeechBubble SpeechBubblePrefab;
    private AlgoSpeechBubble SpeechBubbleInstance;

    [Header("Option")]
    [SerializeField] Transform optionGroup;
    [SerializeField] OptionChoiceBubble OptionChoiceBubblePrefab;
    private OptionChoiceBubble OptionChoiceBubbleInstance;

    [Header("UI")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject infoUI;
    public GameObject nextText;

    public Queue<string> sentences;

    private List<AlgoDialogue> dialogues = new List<AlgoDialogue>();

    private string currenteSentence;
    private AlgoDialogue currentAlgoDialogue = new AlgoDialogue();
    private int currentDialogueIndex = 0;

    private float typingSpeed = 0.05f;
    private bool isOpenUI = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        sentences = new Queue<string>();
    }

    //DialogueOccasion�� �޾Ƽ� algoDialogue�� �ɰ��� �޼ҵ�
    public void TryStartDialogue(AlgoDialogueOccasion dialogueOccasion)
    {
        for(int i = 0; i <= dialogueOccasion.line.y - dialogueOccasion.line.x; i++)
        {
            dialogues.Add(dialogueOccasion.dialogues[i]);
        }
        isOpenUI = true;
        SetUI();
        OnDialogue(dialogues[currentDialogueIndex]);
    }

    //algoDialogue�� context�� Ȯ���� �� ����� ���� �ѱ�� �޼ҵ�
    public void OnDialogue(AlgoDialogue algoDialogue)
    {
        currentAlgoDialogue = algoDialogue;
        //text �� ������ ����
        SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
        SpeechBubbleInstance.speakerName.text = algoDialogue.name;

        sentences.Clear();
        foreach (string line in currentAlgoDialogue.contexts)
        {
            sentences.Enqueue(line);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count != 0)
        {
            currenteSentence = sentences.Dequeue();
            //typingEffect Coroutine
            nextText.SetActive(false);
            StartCoroutine(Typing(currenteSentence));
        }
        else
        {
            if(currentDialogueIndex+1 < dialogues.Count)
            {
                OnDialogue(dialogues[++currentDialogueIndex]);
            }
            else
            {
                isOpenUI = false;
                SetUI();
            }
        }
    }

    IEnumerator Typing(string line)
    {
        SpeechBubbleInstance.text.text = "";
        foreach (char letter in line.ToCharArray())
        {
            SpeechBubbleInstance.text.text += letter;
            SoundManager.instance.Play("typing");
            yield return new WaitForSeconds(typingSpeed);
        }
        AutoScroll.instance.AutoScrolling();
    }

    private void SetUI()
    {
        //UI �Ѱ� ����
        canvasGroup.alpha = (isOpenUI ? 1 : 0);
        canvasGroup.blocksRaycasts = isOpenUI;

        if (!isOpenUI)
        {
            //���� ���� �����
            foreach (Transform child in optionGroup)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in textGroup)
            {
                Destroy(child.gameObject);
            }
            //���� Index �ʱ�ȭ
            dialogues.Clear();
            currentDialogueIndex = 0;
            infoUI.SetActive(true);
            return;
        }

        infoUI.SetActive(false);
    }



    void Update()
    {
        if (isOpenUI)
        {
            if (SpeechBubbleInstance.text.text.Equals(currenteSentence))
            {
                if (sentences.Count != 0)
                {
                    SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
                    SpeechBubbleInstance.speakerName.text = "";
                    NextSentence();
                }
                else if (currentAlgoDialogue.choices.Length > 0)
                {
                    currenteSentence = "";
                    //������ ����
                    OnChoiceOption();
                }
                else
                {
                    nextText.SetActive(true);
                }

            }
        }

    }

    private void OnChoiceOption()
    {
        foreach(AlgoChoiceOption option in currentAlgoDialogue.choices)
        {
            OptionChoiceBubbleInstance = Instantiate(OptionChoiceBubblePrefab, optionGroup);
            OptionChoiceBubbleInstance.option = option;
            OptionChoiceBubbleInstance.text.text = option.option;
        }
    }

    public void ShowOptionChosen(AlgoChoiceOption chosenOne)
    {
        foreach(Transform child in optionGroup)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(ShowOptionCoroutine(chosenOne));

    }

    IEnumerator ShowOptionCoroutine(AlgoChoiceOption chosenOne)
    {
        yield return new WaitForSeconds(0.2f);

        SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
        SpeechBubbleInstance.speakerName.text = "��";
        SpeechBubbleInstance.text.text = chosenOne.option;
        currentDialogueIndex = chosenOne.nextId - 1;
        
        yield return new WaitForSeconds(0.4f);
        AutoScroll.instance.AutoScrolling();
        OnDialogue(dialogues[currentDialogueIndex]);
    }

    //��ư �̺�Ʈ
    public void NextClicked()
    {
        if (dialogues[currentDialogueIndex].isFinal)
        {
            isOpenUI = false;
            SetUI();
            if (currentAlgoDialogue.nextEvent)
            {
                Debug.Log("�̺�Ʈ ���� ���� Ȯ��");
            }
        }
        else
        {
            NextSentence();
        }
    }
}
