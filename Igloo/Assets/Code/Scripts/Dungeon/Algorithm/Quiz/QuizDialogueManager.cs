using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Net.NetworkInformation;

public class QuizDialogueManager : MonoBehaviour, IPointerDownHandler
{
    public static QuizDialogueManager instance;

    public TMP_Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dialogueGroup;
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

    public void OnDialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true;

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
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            SoundManager.instance.Play("typing");
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void Update()
    {
        if(dialogueText.text.Equals(currenteSentence))
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
