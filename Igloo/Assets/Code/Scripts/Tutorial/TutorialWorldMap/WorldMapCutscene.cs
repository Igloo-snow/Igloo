using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WorldMapCutscene : TutorialBase
{
    [SerializeField] private PlayableDirector playableDirector;
    public GameObject knowSecretNpc;
    public float changeTime;
    public GameObject playerBubble;
    public GameObject transitionToUniv;

    public override void Enter()
    {
        playableDirector.Play();
        knowSecretNpc.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            Debug.Log("Á¾·á");
            transitionToUniv.SetActive(true);

            //controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        //playerBubble.SetActive(false);
        //transitionToUniv.SetActive(true);
    }
}
