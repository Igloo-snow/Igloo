using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuestUI : MonoBehaviour
{
    public static bool isCheckingQuest = false;
    private QuestManager questManager;

    [Header("Quest UI")]
    public GameObject questsBoard;
    public Dictionary<string, GameObject> pages;

    [Header("Quests Board")]
    public Transform questButtonGroup;  //content transform
    private Dictionary<string, Button> buttons;
    [SerializeField] private Button btnPrefab;

    [Header("Quest Page")]
    public Transform questPage;
    [SerializeField] private GameObject pagePrefab;

    [Header("SimpleQuset")]
    public Image simpleQuest;
    public Transform simplequestParent;
    private Dictionary<string, Image> simpleQuestMap;

    private void Awake()
    {
        buttons = new Dictionary<string, Button>();
        pages = new Dictionary<string, GameObject>();
        simpleQuestMap = new Dictionary<string, Image>();
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onUpdateQuestUI += UpdateQuestUI;

        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;


    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onUpdateQuestUI -= UpdateQuestUI;

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
            for (int i = 0; i < questManager.startedQuests.Count; i++)
            {
                Quest questTemp = questManager.GetQuestById(questManager.startedQuests[i]);
                if (questTemp.state.Equals(QuestState.FINISHED))
                {
                    buttons[questTemp.info.id].transform.SetAsLastSibling();
                }
            }
        }
    }

    private void StartQuest(string id)
    {
        if (buttons.ContainsKey(id))
        {
            Debug.Log("this quest is already accepted");
            return;
        }

        Quest questTemp = questManager.GetQuestById(id);
        ButtonSetting(questTemp);
        CreateSimpleQuestUI(questTemp);
    }

    private void AdvanceQuest(string id)
    {
        
    }

    private void UpdateQuestUI(string id)
    {
        UpdateSimpleQuestUI(id);
    }

    private void FinishQuest(string id)
    {
        //버튼 관리
        if (buttons.ContainsKey(id))
        {
            buttons[id].image.color = Color.gray;
            buttons[id].transform.SetAsLastSibling();
        }

        //simpleQuest 관리
        RemoveSimpleQuestUI(id);
    }

    private void ButtonSetting(Quest questTemp)
    {
        Button tempbtn = Instantiate(btnPrefab, questButtonGroup.transform);
        

        tempbtn.transform.Find("Title").GetComponent<TMP_Text>().text = questTemp.info.id;
        
        StringBuilder stepDescriptions = new StringBuilder("");
        for (int i = 0; i < questTemp.info.questStepPrefabs.Length; i++)
        {
            stepDescriptions.Append("Step " + (i + 1) + ": " + questTemp.info.questStepPrefabs[i].GetComponent<QuestStep>().stepDescription + "\n");
        }
        //stepDescriptions += questTemp.info.questStepPrefabs[questTemp.currentQuestStepIndex].GetComponent<QuestStep>().stepDescription;
        tempbtn.transform.Find("Detail").GetComponent<TMP_Text>().text = stepDescriptions.ToString();
        
        buttons.Add(questTemp.info.id, tempbtn);
        PageSetting(questTemp, stepDescriptions.ToString());
    }

    private void PageSetting(Quest questTemp, string detail)
    {
        GameObject image = Instantiate(pagePrefab, questPage);
        image.transform.Find("title").GetComponent<TMP_Text>().text = "Re: [" + questTemp.info.displayName + "] 진행 상황";
        image.transform.Find("From").GetComponent<TMP_Text>().text = "보낸 사람 : ";
        image.transform.Find("Detail1").GetComponent<TMP_Text>().text = questTemp.info.id;
        image.transform.Find("Detail2").GetComponent<TMP_Text>().text = detail;
        pages.Add(questTemp.info.id, image);
        pages[questTemp.info.id].gameObject.SetActive(false);
    }

    private void CreateSimpleQuestUI(Quest quest)
    {
        Image simpleQuestUI = Instantiate<Image>(simpleQuest, simplequestParent);
        simpleQuestUI.GetComponent<SimpleQuestUI>().CreateSimpleQuestUI(quest.info.id);
        simpleQuestMap.Add(quest.info.id, simpleQuestUI);

    }

    private void UpdateSimpleQuestUI(string id)
    {

        if (simpleQuestMap.ContainsKey(id))
        {
            simpleQuestMap[id].GetComponent<SimpleQuestUI>().UpdateSimpleQusetUI(id);
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

        simplequestParent.gameObject.SetActive(!isCheckingQuest);
        questsBoard.SetActive(isCheckingQuest);
        GameManager.isOpenQuestUI = isCheckingQuest;
        if (!isCheckingQuest)
        {
            AllPageClose();
        }
    }

    private void AllPageClose()
    {
        foreach (GameObject page in pages.Values)
        {
            page.SetActive(false);
            QuestPageButton.pageActive = false;
        }
    }

    public void PressPageButton()
    {
        Button ClickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string targetString = FindQuestIdByButton(ClickedButton);

        if (QuestPageButton.pageActive == false)
        {
            //바로 켜기
            if (pages.ContainsKey(targetString))
            {
                pages[targetString].gameObject.SetActive(true);
                QuestPageButton.pageActive = true;
            }
        }
        else
        {
            if (pages[targetString].gameObject.activeSelf == true)
            {
                //닫기
                pages[targetString].gameObject.SetActive(false);
                QuestPageButton.pageActive = false;
            }
            else
            {
                // 다른 거 닫고 이거 열기
                AllPageClose();
                pages[targetString].gameObject.SetActive(true);
                QuestPageButton.pageActive = true;

            }
        }
    }

    private string FindQuestIdByButton(Button btn)
    {
        foreach (KeyValuePair<string, Button> pair in buttons)
        {
            if (EqualityComparer<Button>.Default.Equals(pair.Value, btn))
            {
                return pair.Key;
            }
        }
        return null;
    }

}
