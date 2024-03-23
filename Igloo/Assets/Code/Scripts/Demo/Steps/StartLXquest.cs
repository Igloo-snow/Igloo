using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLXquest : StepBase
{
    public QuestInfoSO quest;
    private int nextScene = (int)SceneNames.CutsceneLx;

    public override void Enter()
    {
    }

    public override void Execute(StepController controller)
    {
        if (QuestManager.instance.GetQuestById(quest.id).state == QuestState.IN_PROGRESS)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                controller.SetNextStep();
            }
        }
    }

    public override void Exit()
    {
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextScene);
    }
}
