using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAlarm : TutorialBase
{
    [SerializeField] Phone phone;
    private bool isCompleted = false;

    public override void Enter()
    {
        phone.PlaySound();
        phone.StartVisualEffect();
    }
    
    public void Finish()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted)
        {
            controller.SetNextTutorial();

        }
    }

    public override void Exit()
    {
    }
}
