using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDDquest : StepBase
{
    private int targetItemId = 101;
    private int nextScene = (int)SceneNames.Univ;
    private bool InClassroom;
    public QuestInfoSO quest;

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
        if (InClassroom)
        {
            FindAnyObjectByType<ActionController>().Reposition(this.gameObject);
            InClassroom = false;
        }

    }

    private void Completed()
    {
        isCompleted = true;
    }

    public override void Enter()
    {

    }

    public override void Execute(StepController controller)
    {
        if (InventoryManager.Instance.CheckInventory(targetItemId))
        {
            Invoke("Completed", 1f);
        }
        if (isCompleted && Input.GetKeyDown(KeyCode.Tab))
        {
            controller.SetNextStep();
        }
    }

    public override void Exit()
    {
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextScene);
        QuestManager.instance.GetQuestById(quest.id).state = QuestState.FINISHED;
        InClassroom = true;
        

    }
}
