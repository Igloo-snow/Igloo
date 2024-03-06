using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WorldMapCutscene : TutorialBase
{
    [SerializeField] private PlayableDirector playableDirector;
    public float changeTime;
    public GameObject playerBubble;

    public override void Enter()
    {
        playableDirector.Play();
    }

    public override void Execute(TutorialController controller)
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            Debug.Log("Á¾·á");
            //controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        //playerBubble.SetActive(false);
    }
}
