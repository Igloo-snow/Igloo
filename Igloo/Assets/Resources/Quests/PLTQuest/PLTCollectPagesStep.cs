using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLTCollectPagesStep : QuestStep
{
    [SerializeField] private int[] targetItem;
    private void Update()
    {
        for (int i = 0; i < targetItem.Length; i++)
        {
            if (!InventoryManager.Instance.CheckInventory(targetItem[i]))
                return;
        }
        FinishQuestStep();
    }
}
