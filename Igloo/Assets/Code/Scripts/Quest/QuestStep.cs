using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;

    public void InitializeQuestStep(string questId)
    {

        //나중에 쓰일 일이 있어서 바로 public 변수로 안만들고 이렇게 함수 만들어서
        this.questId = questId;
    }

    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;
            //TODO - 삭제되기 전 다음 스텝 넘어가라고 Advance 신호 보내기
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Debug.Log(questId);

            Destroy(this.gameObject);
        }
    }
}
