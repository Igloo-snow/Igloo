using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [Header("Quest UI")]
    public GameObject simpleQuest;
    public GameObject questsBoard;
    public GameObject questPage;

    private QuestManager questManager;

    [Header("Quests Board")]
    public Transform questButtonGroup;  //content transform
    private Dictionary<string, Button> buttons;
    [SerializeField] private Button btnPrefab;

    [Header("SimpleQuset")]
    public Image checkImg;
    public TMP_Text title;
    public TMP_Text description;

    public bool isCheckingQuest = false;

    private void Start()
    {
        buttons = new Dictionary<string, Button>();
        questManager = FindObjectOfType<QuestManager>();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

    }

    private void OnDisable()
    {
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
    private void StartQuest(string id)
    {
        if(buttons.ContainsKey(id)) {
            Debug.Log("this quest is already accepted");
            return;
        }

        Quest questTemp = questManager.GetQuestById(id);
        ButtonSetting(questTemp);
        SimpleQuestSetting(questTemp);
    }

    private void AdvanceQuest(string id)
    {
        Quest questTemp = questManager.GetQuestById(id);
        SimpleQuestSetting(questTemp);
    }

    private void FinishQuest(string id)
    {
        //버튼 관리
        if ( buttons.ContainsKey(id))
        {
            Destroy(buttons[id].gameObject);
            buttons.Remove(id);
        }

        //simpleQuest 관리
        FinishSimpleQuest(id);
    }

    private void ButtonSetting(Quest questTemp)
    {
        Button tempbtn = Instantiate(btnPrefab, questButtonGroup.transform);
        tempbtn.transform.Find("Title").GetComponent<TMP_Text>().text = questTemp.info.id;
        tempbtn.transform.Find("Detail").GetComponent<TMP_Text>().text = questTemp.info.description;
        buttons.Add(questTemp.info.id, tempbtn);
    }

    private void SimpleQuestSetting(Quest questTemp)
    {
        //체크 이미지 변경
        if (questTemp.state.Equals(QuestState.IN_PROGRESS))
            checkImg.color = Color.white;
        else if (questTemp.state.Equals(QuestState.CAN_FINISH))
            checkImg.color = Color.yellow;
        checkImg.gameObject.SetActive(true);

        //텍스트 변경
        title.text = questTemp.info.id;
        description.text = questTemp.info.description;
    }

    private void FinishSimpleQuest(string id)
    {
        foreach(string questId in buttons.Keys)
        {
            Quest questTemp = questManager.GetQuestById(questId);
            if (questTemp.state.Equals(QuestState.IN_PROGRESS) || questTemp.state.Equals(QuestState.CAN_FINISH))
            {
                SimpleQuestSetting(questTemp);
                return;
            }

        }
        
        checkImg.color = Color.white;
        checkImg.gameObject.SetActive(true);
        title.text = "진행중인 퀘스트가 없습니다";
        description.text = null;
    }

    public void QuestCheking()
    {
        isCheckingQuest = !isCheckingQuest;
        simpleQuest.SetActive(!isCheckingQuest);
        questsBoard.SetActive(isCheckingQuest);
    }
    
}
