using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetNunsongQuestStep : QuestStep
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
}
