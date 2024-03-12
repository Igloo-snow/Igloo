using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    private Dictionary<string, Quest> questMap;
    public List<string> startedQuests = new List<string>();
    public List<string> inProgressQuests = new List<string>();
    private string currentScene;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if(DataManager.instance.nowPlayer.quests.Count > 0)
            {
                questMap = LoadQuestData();
            }
            else
            {
                questMap = CreateQuestMap();
            }
            currentScene = SceneManager.GetActiveScene().name;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        /*foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }*/
    }

    private void Update()
    {
        foreach(Quest quest in questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StepSetActive(scene.name);

    }

    private void StepSetActive(string sceneName) {
        int stepCount = transform.childCount;
        for(int i = 0; i < stepCount; i++)
        {
            if(transform.GetChild(i).GetComponent<QuestStep>().targetScene != "")
            {
                if (transform.GetChild(i).GetComponent<QuestStep>().targetScene == sceneName)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }

    }
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        if(quest.info.questPrerequisites.Length > 0)
        {
            foreach(QuestInfoSO pre in quest.info.questPrerequisites)
            {
                if(GetQuestById(pre.id).state != QuestState.FINISHED)
                {
                    meetsRequirements = false;
                }
            }
        }
        return meetsRequirements;
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrnetQuestStep(this.transform);
        StepSetActive(currentScene);

        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        startedQuests.Add(id);
        inProgressQuests.Add(id);

        SoundManager.instance.Play("02_Heal_02");
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrnetQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            GameEventsManager.instance.dialogueEvents.UpdateDialogueIndex(quest.info.npcId);
        }

        GameEventsManager.instance.questEvents.UpdateQuestUI(id);
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        if (inProgressQuests.Contains(id))
        {
            inProgressQuests.Remove(id);
        }
        else
        {
            Debug.Log(id + " InprogressQuests 오류. 시작이 확인되지 않고 종료됨");
        }

    }

    private void ClaimRewards(Quest quest)
    {
        Debug.Log(quest.info.id + " 퀘스트에 대한 보상 지급");
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.Log("중복되는 id 발견");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    public Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("없는 id: " + id);
        }
        return quest;
    }

    public void SaveQuestData()
    {
        foreach(Quest quest in questMap.Values)
        {
            quest.SaveQuestData();
        }
        
    }

    public Dictionary<string, Quest> LoadQuestData()
    {
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (Quest quest in DataManager.instance.nowPlayer.quests)
        {
            if (idToQuestMap.ContainsKey(quest.info.id))
            {
                Debug.Log("중복되는 id 발견");
            }
            Quest newQ = new Quest(quest.info);
            newQ.LoadQuestData(quest);
            idToQuestMap.Add(quest.info.id, newQ);
            CheckState(newQ);
        }
        return idToQuestMap;

    }

    private void CheckState(Quest quest)
    {
        if (quest.state > QuestState.CAN_START)
            startedQuests.Add(quest.info.id);

        if (quest.state == QuestState.IN_PROGRESS)
        {
            inProgressQuests.Add(quest.info.id);
            if (quest.CurrentStepExists())
                quest.InstantiateCurrnetQuestStep(this.transform);
        }
        else if(quest.state == QuestState.CAN_FINISH)
        {
            inProgressQuests.Add(quest.info.id);

        }

    }
}
