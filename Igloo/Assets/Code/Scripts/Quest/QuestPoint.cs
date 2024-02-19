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
        //새로 시작할때마다 questManager에서 현 상태 받아오기
        questManager = FindObjectOfType<QuestManager>();
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
       
        if (dialogueTrigger.dialogue.dialogueId.Equals(id) && playerIsNear)
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
            Debug.Log("[ " + questId + " ]" + " 퀘스트가 시작되었습니다");
        }

    }

    public void ClearQuest()
    {
        if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
            Debug.Log("[ " + questId + " ]" + " 퀘스트를 완수했습니다");
        }
    }
}
