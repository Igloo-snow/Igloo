using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    public string currentScene;
    public int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrnetQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            //QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>(); ���� ���� �ڵ�
            QuestStep questStep = GameObject.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            
            questStep.InitializeQuestStep(info.id);
        }
        else
        {
            Debug.Log("����Ʈ ���� ������ null");
        }
    }

    private void QuestStepSetActive(QuestStep questStep)
    {
        if(questStep.targetScene == currentScene)
        {
            questStep.gameObject.SetActive(true);
        }
        else
        {
            questStep.gameObject.SetActive(false);
        }
        
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.Log(info.id + "�� ���� step prefab ����");
        }

        return questStepPrefab;
    }

    public void SaveQuestData()
    {
        foreach(Quest quest in DataManager.instance.nowPlayer.quests)
        {
            if(quest.info.id == this.info.id)
            {
                //�ٲ� ������ ����
                quest.state = this.state;
                quest.currentQuestStepIndex = currentQuestStepIndex;
                return;
            }
        }
        DataManager.instance.nowPlayer.quests.Add(this);
    }

    public void LoadQuestData(Quest quest)
    {
        this.state = quest.state;
        this.currentQuestStepIndex = quest.currentQuestStepIndex;
    }

}
