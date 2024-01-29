using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{

    private int itemsCollected = 0;
    private int itemsToComplete = 5;

    private void Awake()
    {
    
    }

    private void Update()
    {
        if(itemsCollected >= itemsToComplete)
        {
            Debug.Log("finish");
            FinishQuestStep();
        }
    }
}
