using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearEndCreditsNpc : StepBase
{
    public QuestInfoSO lxQuest;
    private GameObject endcreditsNpc;

    public override void Enter()
    {
    }

    public override void Execute(StepController controller)
    {
        if(QuestManager.instance.GetQuestById(lxQuest.id).state == QuestState.FINISHED) 
        {
            endcreditsNpc = GameObject.FindWithTag("Respawn").transform.Find("EndCreditsNPC").gameObject;
            endcreditsNpc.SetActive(true);
            controller.SetNextStep();
        }
    }

    public override void Exit()
    {
    }
}
