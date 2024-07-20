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
    [SerializeField] private AlgoSpeechBubble SpeechBubblePrefab;
    private AlgoSpeechBubble SpeechBubbleInstance;

    [Header("Option")]
    [SerializeField] Transform optionGroup;
    [SerializeField] private OptionChoiceBubble OptionChoiceBubblePrefab;
    private OptionChoiceBubble OptionChoiceBubbleInstance;

    [Header("UI")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject infoUI;
    public GameObject nextText;

    public Queue<string> sentences;

    private List<AlgoDialogue> dialogues = new List<AlgoDialogue>();

    private string currenteSentence;
    private AlgoDialogue currentAlgoDialogue = new AlgoDialogue();
    private int currentDialogueIndex = 0;

    private float typingSpeed = 0.05f;
    private bool isTyping;
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
        SetUI(isOpenUI);
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
            isTyping = true;
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
                SetUI(isOpenUI);
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
    }

    private void SetUI(bool setting)
    {
        //UI �Ѱ� ����
        canvasGroup.alpha = (setting ? 1 : 0);
        canvasGroup.blocksRaycasts = setting;

        if (!setting)
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
                if (currentAlgoDialogue.choices.Length > 0)
                {
                    currenteSentence = "";
                    //������ ����
                    OnChoiceOption();
                }
                else
                {
                    nextText.SetActive(true);
                    isTyping = false;
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
        OnDialogue(dialogues[currentDialogueIndex]);
    }

    public void NextClicked()
    {
        if (!isTyping)
        {
            if (dialogues[currentDialogueIndex].isFinal)
            {
                isOpenUI = false;
                SetUI(isOpenUI);
            }
            else
            {
                NextSentence();
            }
        }
    }

}
