using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;

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
            //TODO - �����Ǳ� �� ���� ���� �Ѿ��� Advance ��ȣ ������
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Debug.Log(questId);

            Destroy(this.gameObject);
        }
    }
}
