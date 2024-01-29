using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPageButton : MonoBehaviour
{
    private QuestUI questUI;

    private bool isActive = false;

    //public GameObject page;

    private void Start()
    {
        questUI = FindObjectOfType<QuestUI>();
    }
    private void Update()
    {
        //작동 안함

    }
    public void PressButton()
    {
        if(isActive == false)
        {
            isActive = true;
            Debug.Log("isActive");
            questUI.OpenQuestPage();
        }
        else
        {
            isActive = false;
            questUI.CloseQuestPage();
        }
    }
}
