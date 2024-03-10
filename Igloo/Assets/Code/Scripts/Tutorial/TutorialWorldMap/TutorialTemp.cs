using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTemp : TutorialBase
{
    private bool isCompleted;
    public GameObject beforeCutsceneNpc;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
            isCompleted = true;
    }
    public override void Enter()
    {
        beforeCutsceneNpc.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted)
        {
            controller.SetNextTutorial();
            Debug.Log("temp Á¾·á");
        }
    }

    public override void Exit()
    {
        beforeCutsceneNpc.SetActive(false);
    }

}
