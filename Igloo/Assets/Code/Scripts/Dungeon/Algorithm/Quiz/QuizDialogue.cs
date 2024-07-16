using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizDialogue : DialogueStepBase
{
    public TMP_Text dialogueText;
    public GameObject nextText;
    public Queue<string> sentences;
    public QuizSentence quizSentence;

    private string currenteSentence;

    public float typingSpeed = 0.1f;
    private bool isTyping;
    private bool isDone = false;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public override void Enter()
    {
        sentences.Clear();
        foreach (string line in quizSentence.sentences)
        {
            sentences.Enqueue(line);
        }
        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currenteSentence = sentences.Dequeue();
            //typingEffect Coroutine
            isTyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currenteSentence));
        }
        else
        {
            isDone = true;
        }
    }

    public override void Execute(DialogueHandler handler)
    {
        if (dialogueText.text.Equals(currenteSentence))
        {
            nextText.SetActive(true);
            isTyping = false;
        }
        if (isDone)
        {
            handler.SetNextDialogue();
        }
    }

    IEnumerator Typing(string line)
    {
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            SoundManager.instance.Play("typing");
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogueText.text += "\n";

    }

    public override void Exit()
    {
    }
}
