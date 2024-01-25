using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject visualCue;
    public bool isFirst;
    private QuestPoint questPoint;
    private bool isPlayerInRange;

    private void Awake()
    {
        isPlayerInRange = false;
        isFirst = true;
        visualCue.SetActive(false);

    }

    private void Start()
    {
        questPoint = GetComponent<QuestPoint>(); // caution
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }

        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && isFirst)
            {
                TriggerDialogue();
                questPoint.AcceptQuest();
                isFirst = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

   /* private void OnTriggerStay(Collider collider)
    {
        if  (collider.CompareTag("Player"))
        {
            
        }
    }*/

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
