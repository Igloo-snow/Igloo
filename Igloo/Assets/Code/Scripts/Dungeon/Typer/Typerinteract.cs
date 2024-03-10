using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typerinteract : Interactable
{
    public GameObject specificUI;

    private void Awake()
    {
        specificUI.SetActive(false);
        promptMessage = "E키로 말 걸기";
    }

    private void Update()
    {

    }

    protected override void Interact()
    {
        Debug.Log("enter");
        if (!specificUI.activeSelf)
            {
            if (specificUI.GetComponent<UiBase>())
            {
                UiManager.instance.CheckUi(specificUI.GetComponent<UiBase>());
            }
            else
               specificUI.SetActive(true);
            }
    }
}
