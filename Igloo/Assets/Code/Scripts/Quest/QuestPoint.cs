using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfoSO questInfoForPoint;

    [Header("Type")]
    [SerializeField] private bool startPoint;
    [SerializeField] private bool finishPoint;

    [Header("Dialogue")]
    [SerializeField] private DialogueTrigger dialogueTrigger;

    public string questId;
    private QuestState currentQuestState;
    private QuestManager questManager;

    private bool playerIsNear = false;

    private void Start()
    {
        questId = questInfoForPoint.id;
        questManager = FindObjectOfType<QuestManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        currentQuestState = questManager.GetQuestById(questId).state;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.dialogueEvents.onFinishDialogue += FinishDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.dialogueEvents.onFinishDialogue -= FinishDialogue;
    }

    private void FinishDialogue(int id)
    {
        //Debug.Log("dialogueTrigger.index" + dialogueTrigger.index);
        //Debug.Log("존나긴거" + dialogueTrigger.dialogues[dialogueTrigger.index].dialogueId);
        if (playerIsNear && dialogueTrigger.dialogues[dialogueTrigger.index].dialogueId.Equals(id))
        {
            //Debug.Log(id + dialogueTrigger.dialogue.dialogueId);
            //Debug.Log(transform.name + startPoint.ToString() + "     " + finishPoint.ToString());
            if(startPoint)
            {
                AcceptQuest();
            }
            if (finishPoint)
            {
                ClearQuest();
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("In QuestPoint : " + questId + currentQuestState);
        }
    }
    public void AcceptQuest()
    {
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            Debug.Log("[ " + questId + " ]" + " ����Ʈ�� ���۵Ǿ����ϴ�");
        }

    }

    public void ClearQuest()
    {
        if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
            Debug.Log("[ " + questId + " ]" + " ����Ʈ�� �ϼ��߽��ϴ�");
        }
    }
}
