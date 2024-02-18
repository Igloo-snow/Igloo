using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    private string currentDialogue;
    public bool isPlaying { get; private set; }
    private static DialogueManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    void Start()
    {
        isPlaying = false;
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isPlaying = true;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        currentDialogue = dialogue.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        GameEventsManager.instance.dialogueEvents.FinishDialogue(currentDialogue);
        isPlaying = false;
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
