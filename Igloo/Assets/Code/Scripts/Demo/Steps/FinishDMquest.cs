using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDMquest : StepBase
{
    public QuestInfoSO quest;
    private int nextScene = (int)SceneNames.Univ;
    private bool aroundLXnpc = false;
    public Item dmBook;
    public Item dmPage;

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
        if( Input.GetKeyDown(KeyCode.Tab)) 
        {
            SoundManager.instance.Play("56_Attack_03");

            controller.SetNextStep();
        }
    }

    public override void Exit()
    {
        //��ũ��Ʈ�� ���� ���� �� NPC ��ȭ �ε��� ������Ʈ �ȵ�
        QuestManager.instance.GetQuestById(quest.id).state = QuestState.FINISHED;
        //������ ����
        InventoryManager.Instance.Add(dmBook);
        if(InventoryManager.Instance.CheckInventory(dmPage.id))    
            InventoryManager.Instance.Remove(dmPage);
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextScene);
        aroundLXnpc = true;
    }
}
