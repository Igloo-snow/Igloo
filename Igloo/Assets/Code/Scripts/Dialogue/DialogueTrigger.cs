using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public int npcId;
    public List<Dialogue> dialogues;
    public int index;
    public GameObject visualCue;
    public bool isFirst;

    private QuestPoint questPoint;
    [SerializeField]
    private bool isQuestRelated;
    public bool isPlayerInRange;

    //public Image cueImage;
    public Transform target;

    private void Awake()
    {
        isPlayerInRange = false;
        isFirst = true;
        //visualCue.SetActive(false);
        //cueImage.enabled = false;
        index = 0;
    }

    private void Start()
    {
        //questPoint = GetComponent<QuestPoint>(); // caution
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            //visualCue.SetActive(true);
            //cueImage.enabled = true;
            if (!DialogueManager.GetInstance().isPlaying)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (isFirst && isQuestRelated)
                    {
                        if (isFirst)
                        {
                            Debug.Log(npcId);
                            GameEventsManager.instance.dialogueEvents.StartDialogue(npcId);
                            TriggerDialogue();
                            //questPoint.AcceptQuest();
                        }
                        //questPoint.ClearQuest();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
        else
        {
            //visualCue.SetActive(false);
            //cueImage.enabled = false;
        }

        //cueImage.transform.position = Camera.main.WorldToScreenPoint(target.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues[index]);
    }
    
    public void setIsFirstFalse()
    {
        isFirst = false;
    }
}
