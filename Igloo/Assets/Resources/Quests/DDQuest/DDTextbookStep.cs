using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDTextbookStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FinishQuestStep();
            GetComponent<QuestPoint>().ClearQuest();
        }
    }
}