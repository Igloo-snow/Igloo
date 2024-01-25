using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]
    private QuestInfoSO questInfoForPoint;

    [Header("Type")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;


    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log(questId + currentQuestState);
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
