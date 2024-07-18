using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Net.NetworkInformation;

public class AlgoSpeechBubbleManager : MonoBehaviour, IPointerDownHandler
{
    public static AlgoSpeechBubbleManager instance;

    [SerializeField] Transform textGroup;
    private AlgoSpeechBubble SpeechBubblePrefab;
    private AlgoSpeechBubble SpeechBubbleInstance;

    public GameObject nextText;
    public Queue<string> sentences;

    private string currenteSentence;

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

    public void OnDialogue(string[] lines, string name)
    {
        //text 들어갈 프리팹 생성
        SpeechBubbleInstance = Instantiate(SpeechBubblePrefab, textGroup);
        SpeechBubbleInstance.speakerName.text = name;

        // 
        sentences.Clear();
        foreach (string line in lines)
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
            //종료
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
            nextText.SetActive(true);
            isTyping = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isTyping)
            NextSentence();
    }
}
