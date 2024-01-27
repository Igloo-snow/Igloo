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

        //���߿� ���� ���� �־ �ٷ� public ������ �ȸ���� �̷��� �Լ� ����
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
