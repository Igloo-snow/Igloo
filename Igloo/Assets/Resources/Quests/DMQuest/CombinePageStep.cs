using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinePageStep : QuestStep
{
    [SerializeField] private BookItem bookItem;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            bookItem.gameObject.SetActive(true);
            bookItem.CombinePages();
        }
    }

    private void Update()
    {
        if (bookItem.isFinish)
        {
            FinishQuestStep();
        }
    }
}
