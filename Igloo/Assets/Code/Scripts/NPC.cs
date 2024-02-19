using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool doublePoint = true;
    [SerializeField] private GameObject beforeNpc;
    [SerializeField] private GameObject afterNpc;

    private QuestPoint questPoint;


    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestUI += UpdateQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onUpdateQuestUI -= UpdateQuest;
    }

    private void Start()
    {
        questPoint = GetComponentInChildren<QuestPoint>();
    }

    private void UpdateQuest(string questName)
    {
        if (doublePoint)
        {
            bool canFinish = !(QuestManager.instance.GetQuestById(questName).CurrentStepExists());
            if (questPoint.questId.Equals(questName) && canFinish)
            {
                beforeNpc.SetActive(false);
                afterNpc.SetActive(true);
            }
        }

    }



}
