using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    public string targetScene;
    public string stepDescription;

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

            GameEventsManager.instance.questEvents.AdvanceQuest(questId);

            Destroy(this.gameObject);
        }
    }
}
