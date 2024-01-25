using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject visualCue;
    public bool isFirst;

    private QuestPoint questPoint;
    [SerializeField]
    private bool isQuestRelated;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isFirst && isQuestRelated)
                {
                    TriggerDialogue();
                    questPoint.AcceptQuest();
                    isFirst = false;
                }
                else if (isFirst && !isQuestRelated)
                {
                    TriggerDialogue();
                    isFirst = false;
                }
                else if (!isFirst && isQuestRelated)
                {
                    questPoint.ClearQuest();
                }

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
