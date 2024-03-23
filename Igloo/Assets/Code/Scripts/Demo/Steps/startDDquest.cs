using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDDquest : StepBase
{
    private int count = 1;
    public int npcId = 1;
    private int nextScene = (int)SceneNames.CutsceneDD;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onFinishDialogue += FinishDialogue; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onFinishDialogue -= FinishDialogue;

    }

    private void FinishDialogue(int id)
    {
        if(id == npcId && count == 1)
        {
            count = 0;
            Invoke("complete", 1f);
        }
    }

    private void complete()
    {
        isCompleted = true;
    }

    public override void Enter()
    {

    }

    public override void Execute(StepController controller)
    {
        if (isCompleted && Input.GetKeyDown(KeyCode.Tab))
        {
            controller.SetNextStep();
        }
    }

    public override void Exit()
    {
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextScene);
    }
}
