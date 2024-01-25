using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPageButton : MonoBehaviour
{
    private bool isActive = false;

    public GameObject parent;
    public GameObject page;

    private QuestUI ui;
    //public GameObject page;

    private void Start()
    {
        ui = FindObjectOfType<QuestUI>();
    }
    private void Update()
    {
        //작동 안함
        if (!ui.isCheckingQuest)
            isActive = false;
    }
    public void PressButton()
    {
        isActive = !isActive;
        this.gameObject.SetActive(isActive);
    }
}
