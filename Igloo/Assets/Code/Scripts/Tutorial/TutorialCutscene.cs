using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialCutscene : TutorialBase
{
    public PlayableDirector playableDirector;
    private bool isCompleted = false;

    public override void Enter()
    {
        playableDirector.Play();
        while (true)
        {
            
        }
    }

    public override void Execute(TutorialController controller)
    {

    }

    public override void Exit()
    {
    }
}
