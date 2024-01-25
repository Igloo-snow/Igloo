using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private ActionController ac;

    //private int itemsCollected = 0;
    private int itemsToComplete = 5;

    private void Awake()
    {
        ac = FindObjectOfType<ActionController>();
    }

    private void Update()
    {
        if(ac.coins >= itemsToComplete)
        {
            Debug.Log("finish");
            FinishQuestStep();
            ac.coins = 0;
        }
    }
}
