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

    private int currentDialogueId;
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
        UiManager.isDialogueOpen = true;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        currentDialogueId = dialogue.dialogueId;
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
        GameEventsManager.instance.dialogueEvents.FinishDialogue(currentDialogueId);
        isPlaying = false;
        animator.SetBool("IsOpen", false);
        StartCoroutine(DialogueCloseCoroutine());
    }

    IEnumerator DialogueCloseCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        UiManager.isDialogueOpen = false;

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
