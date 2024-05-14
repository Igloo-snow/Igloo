using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brochure : Interactable
{
    public UiBase uiBase;
    [SerializeField] private BlinkingObject blinkingObject;

    private bool canOpen;

    protected override void Interact()
    {
        if(canOpen)
            UiManager.instance.CheckUi(uiBase);

        if (blinkingObject != null)
            blinkingObject.StopBlinking();

        promptMessage = "컴퓨터과학전공의 커리큘럼이다!";


    }

    public void CloseUi()
    {
        UiManager.instance.CheckUi(uiBase);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
