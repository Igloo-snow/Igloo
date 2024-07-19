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

    [SerializeField] Transform textGroup;
    [SerializeField] private AlgoSpeechBubble SpeechBubblePrefab;
    private AlgoSpeechBubble SpeechBubbleInstance;

    [SerializeField] Transform optionGroup;
    [SerializeField] private OptionChoiceBubble OptionChoiceBubblePrefab;
    private OptionChoiceBubble OptionChoiceBubbleInstance;

    public GameObject nextText;
    public Queue<string> sentences;

    private List<AlgoDialogue> dialogues = new List<AlgoDialogue>();

    private string currenteSentence;
    private AlgoDialogue currentAlgoDialogue = new AlgoDialogue();
    private int currentDialogueIndex = 0;

    public float typingSpeed = 0.1f;
    private bool isTyping;

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
        Debug.Log(dialogues.Count); 
        OnDialogue(dialogues[currentDialogueIndex]);
    }

    //algoDialogue�� context�� Ȯ���� �� ����� ���� �ѱ�� �޼ҵ�
    public void OnDialogue(AlgoDialogue algoDialogue)
    {
        currentAlgoDialogue = algoDialogue;
        //text �� ������ ����
        SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
        SpeechBubbleInstance.speakerName.text = algoDialogue.name;

        // 
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
                Debug.Log("����");
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

    void Update()
    {
        if(SpeechBubbleInstance.text.text.Equals(currenteSentence))
        {
            if(currentAlgoDialogue.choices.Length > 0)
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

        SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
        SpeechBubbleInstance.speakerName.text = "��";
        SpeechBubbleInstance.text.text = chosenOne.option;
        currentDialogueIndex = chosenOne.nextId - 1;
        OnDialogue(dialogues[currentDialogueIndex]);

    }

    public void NextClicked()
    {
        if (!isTyping)
        {
            // ������ ����ü � �� Ȯ���ؾߵǳ�
            if (dialogues[currentDialogueIndex].isFinal)
            {
                //��ȭ ����
                Debug.Log("��ȭ ����");
            }
            else
            {
                Debug.Log("nextSentence �޼ҵ� ȣ��");
                NextSentence();
            }
        }
    }

}
