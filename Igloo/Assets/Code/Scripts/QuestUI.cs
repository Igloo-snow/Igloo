using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class QuestUI : MonoBehaviour
{
    [Header("Quest UI")]
    public GameObject questsBoard;
    public GameObject questPage;

    private QuestManager questManager;

    [Header("Quests Board")]
    public Transform questButtonGroup;  //content transform
    private Dictionary<string, Button> buttons;
    [SerializeField] private Button btnPrefab;

    [Header("SimpleQuset")]
    public Image simpleQuest;
    public Transform parent;
    private Dictionary<string, Image> simpleQuestMap;

    public bool isCheckingQuest = false;

    private void Awake()
    {
        buttons = new Dictionary<string, Button>();
        simpleQuestMap = new Dictionary<string, Image>();
        questManager = FindObjectOfType<QuestManager>();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestCheking();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //진행중인 퀘스트 확인 후 UI 세팅
        if (questManager.startedQuests.Count > 0)
        {
            for (int i = 0; i < questManager.startedQuests.Count; i++)
            {
                Quest questTemp = questManager.GetQuestById(questManager.startedQuests[i]);

                if (questTemp.state.Equals(QuestState.FINISHED))
                {
                    ButtonSetting(questTemp);
                    buttons[questTemp.info.id].image.color = Color.gray;
                }
                else if (questTemp.state.Equals(QuestState.IN_PROGRESS))
                {
                    ButtonSetting(questTemp);
                    CreateSimpleQuestUI(questTemp);
                }
                else if (questTemp.state.Equals(QuestState.CAN_FINISH))
                {
                    ButtonSetting(questTemp);
                    CreateSimpleQuestUI(questTemp);
                    UpdateSimpleQuestUI(questTemp.info.id);

                }
            }
        }
    }

    private void StartQuest(string id)
    {
        if(buttons.ContainsKey(id)) {
            Debug.Log("this quest is already accepted");
            return;
        }

        Quest questTemp = questManager.GetQuestById(id);
        ButtonSetting(questTemp);
        CreateSimpleQuestUI(questTemp);
    }

    private void AdvanceQuest(string id)
    {
        UpdateSimpleQuestUI(id);
    }

    private void FinishQuest(string id)
    {
        //버튼 관리
        if ( buttons.ContainsKey(id))
        {
            buttons[id].image.color = Color.gray; 
        }

        //simpleQuest 관리
        RemoveSimpleQuestUI(id);
    }

    private void ButtonSetting(Quest questTemp)
    {
        Button tempbtn = Instantiate(btnPrefab, questButtonGroup.transform);
        tempbtn.transform.Find("Title").GetComponent<TMP_Text>().text = questTemp.info.id;
        tempbtn.transform.Find("Detail").GetComponent<TMP_Text>().text = questTemp.info.description;
        buttons.Add(questTemp.info.id, tempbtn);
    }

    private void CreateSimpleQuestUI(Quest quest)
    {
        Image simpleQuestUI = Instantiate<Image>(simpleQuest, parent);
        simpleQuestUI.transform.Find("Title").GetComponent<TMP_Text>().text = quest.info.id;
        simpleQuestUI.transform.Find("Detail").GetComponent<TMP_Text>().text = quest.info.description;
        simpleQuestUI.transform.Find("Check").GetComponent<Image>().color = Color.white;
        simpleQuestMap.Add(quest.info.id, simpleQuestUI);
    }

    private void UpdateSimpleQuestUI(string id)
    {
        if(simpleQuestMap.ContainsKey(id))
        {
            simpleQuestMap[id].transform.Find("Check").GetComponent<Image>().color = Color.yellow;
        }
    }

    private void RemoveSimpleQuestUI(string id)
    {
        if (simpleQuestMap.ContainsKey(id))
        {
            Destroy(simpleQuestMap[id].gameObject);
            simpleQuestMap.Remove(id);
        }
    }

    public void QuestCheking()
    {
        isCheckingQuest = !isCheckingQuest;
        parent.gameObject.SetActive(!isCheckingQuest);
        questsBoard.SetActive(isCheckingQuest);
    }
    
}
