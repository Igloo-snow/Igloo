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
    private QuestManager questManager;

    [Header("Quest UI")]
    public GameObject questsBoard;
    public Dictionary<string, GameObject> pages;

    [Header("Quests Board")]
    public Transform questButtonGroup;  //content transform
    public Transform questPage;
    private Dictionary<string, Button> buttons;
    [SerializeField] private Button btnPrefab;
    [SerializeField] private GameObject pagePrefab;

    [Header("SimpleQuset")]
    public Image simpleQuest;
    public Transform parent;
    private Dictionary<string, Image> simpleQuestMap;

    public bool isCheckingQuest = false;

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
        //�������� ����Ʈ Ȯ�� �� UI ����
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
        UpdateSimpleQuestUI(id);
    }

    private void FinishQuest(string id)
    {
        //��ư ����
        if (buttons.ContainsKey(id))
        {
            buttons[id].image.color = Color.gray;
            buttons[id].transform.SetAsLastSibling();
        }

        //simpleQuest ����
        RemoveSimpleQuestUI(id);
    }

    private void ButtonSetting(Quest questTemp)
    {
        Button tempbtn = Instantiate(btnPrefab, questButtonGroup.transform);
        GameObject image = Instantiate(pagePrefab, questPage);

        tempbtn.transform.Find("Title").GetComponent<TMP_Text>().text = questTemp.info.id;
        image.transform.Find("title").GetComponent<TMP_Text>().text = "Re: [" + questTemp.info.displayName + "] ���� ��Ȳ";
        image.transform.Find("From").GetComponent<TMP_Text>().text = "���� ��� : ";
        image.transform.Find("Detail1").GetComponent<TMP_Text>().text = questTemp.info.id;
        StringBuilder stepDescriptions = new StringBuilder("");
        for (int i = 0; i < questTemp.info.questStepPrefabs.Length; i++)
        {
            stepDescriptions.Append("Step " + (i + 1) + ": " + questTemp.info.questStepPrefabs[i].GetComponent<QuestStep>().stepDescription + "\n");
        }
        //stepDescriptions += questTemp.info.questStepPrefabs[questTemp.currentQuestStepIndex].GetComponent<QuestStep>().stepDescription;
        tempbtn.transform.Find("Detail").GetComponent<TMP_Text>().text = stepDescriptions.ToString();
        image.transform.Find("Detail2").GetComponent<TMP_Text>().text = stepDescriptions.ToString();
        buttons.Add(questTemp.info.id, tempbtn);
        pages.Add(questTemp.info.id, image);
        pages[questTemp.info.id].gameObject.SetActive(false);


    }

    private void CreateSimpleQuestUI(Quest quest)
    {
        Image simpleQuestUI = Instantiate<Image>(simpleQuest, parent);
        simpleQuestUI.transform.Find("Title").GetComponent<TMP_Text>().text = quest.info.id;
        if (quest.state.Equals(QuestState.CAN_FINISH))

        {
            simpleQuestUI.transform.Find("Detail").GetComponent<TMP_Text>().text = quest.info.questStepPrefabs[quest.currentQuestStepIndex - 1].GetComponent<QuestStep>().stepDescription;
            simpleQuestUI.transform.Find("Check").GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            simpleQuestUI.transform.Find("Detail").GetComponent<TMP_Text>().text = quest.info.questStepPrefabs[quest.currentQuestStepIndex].GetComponent<QuestStep>().stepDescription;
            simpleQuestUI.transform.Find("Check").GetComponent<Image>().color = Color.white;

        }
        simpleQuestMap.Add(quest.info.id, simpleQuestUI);

    }

    private void UpdateSimpleQuestUI(string id)
    {
        //���� ���� ������ ������ �װɷ� �󼼳��� �ٲٱ�
        //������ üũ �� ����
        if (simpleQuestMap.ContainsKey(id))
        {
            Quest tempQuest = questManager.GetQuestById(id);

            if (tempQuest.state.Equals(QuestState.CAN_FINISH))
            {
                simpleQuestMap[id].transform.Find("Check").GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                QuestStep questStep = tempQuest.info.questStepPrefabs[tempQuest.currentQuestStepIndex].GetComponent<QuestStep>();
                simpleQuestMap[id].transform.Find("Detail").GetComponent<TMP_Text>().text = questStep.stepDescription;
            }
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

    public void OpenQuestPage()
    {
        Button ClickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string targetString = FindQuestIdByButton(ClickedButton);
        if (pages.ContainsKey(targetString))
        {
            pages[targetString].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("�ش��ϴ� page�� �������� �ʾҽ��ϴ�");
        }

    }
    
    public void CloseQuestPage()
    {
        Button ClickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string targetString = FindQuestIdByButton(ClickedButton);
        if (pages.ContainsKey(targetString))
        {
            pages[targetString].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�ش��ϴ� page�� �������� �ʾҽ��ϴ�");
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
