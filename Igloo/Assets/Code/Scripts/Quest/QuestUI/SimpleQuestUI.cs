using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleQuestUI : MonoBehaviour
{
    private TMP_Text title;
    private TMP_Text detail;
    private Image check;

    private Quest relatedQuest;


    private void InitUI()
    {
        title = transform.Find("Title").GetComponent<TMP_Text>();
        detail = transform.Find("Detail").GetComponent<TMP_Text>();
        check = transform.Find("Check").GetComponent<Image>();
    }

    public void CreateSimpleQuestUI(string questId)
    {
        Debug.Log(questId);
        InitUI();
        Quest relatedQuest = QuestManager.instance.GetQuestById(questId);

        title.text = relatedQuest.info.displayName;

        if (relatedQuest.state.Equals(QuestState.CAN_FINISH))
        {
            detail.text = relatedQuest.info.questStepPrefabs[relatedQuest.currentQuestStepIndex - 1].GetComponent<QuestStep>().stepDescription;
            check.color = Color.yellow;
        }
        else
        {
            detail.text = relatedQuest.info.questStepPrefabs[relatedQuest.currentQuestStepIndex].GetComponent<QuestStep>().stepDescription;
            check.color = Color.white;

        }
    }

    public void UpdateSimpleQusetUI(string questId)
    {
        InitUI();
        Quest relatedQuest = QuestManager.instance.GetQuestById(questId);

        if (relatedQuest.state.Equals(QuestState.CAN_FINISH))
        {
            check.color = Color.yellow;
        }
        else
        {
            QuestStep questStep = relatedQuest.info.questStepPrefabs[relatedQuest.currentQuestStepIndex].GetComponent<QuestStep>();
            detail.text = questStep.stepDescription;
        }
    }

}
