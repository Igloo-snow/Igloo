using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TutorialHomeCutscene : TutorialBase
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutscenePlayer;
    [SerializeField] private PlayableDirector playableDirector;
    public float changeTime;
    public override void Enter()
    {
        player.SetActive(false);
        playableDirector.Play();
    }

    public override void Execute(TutorialController controller)
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            player.SetActive(true);
            cutscenePlayer.SetActive(false);
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        playableDirector.Stop();

    }

}
