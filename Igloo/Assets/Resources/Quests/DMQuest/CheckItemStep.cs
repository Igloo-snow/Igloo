using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItemStep : QuestStep
{
    [SerializeField] private int targetItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (FindObjectOfType<InventoryManager>().CheckInventory(targetItem))
            {
                Debug.Log(targetItem + " 확인 완료");
                FinishQuestStep();
            }

        }
    }
}
