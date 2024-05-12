using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLXquest : StepBase
{
    private bool aroundLXnpc = false;
    public QuestInfoSO quest;
    public Item LXbook;
    private int nextScene = (int)SceneNames.Univ;


    public override void Enter()
    {
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (aroundLXnpc)
        {
            FindAnyObjectByType<ActionController>().Reposition(this.gameObject);
            aroundLXnpc = false;
        }

    }
    public override void Execute(StepController controller)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SoundManager.instance.Play("56_Attack_03");

            controller.SetNextStep();
        }
    }

    public override void Exit()
    {
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextScene);
        InventoryManager.Instance.Add(LXbook);
        aroundLXnpc = true;
    }
}
