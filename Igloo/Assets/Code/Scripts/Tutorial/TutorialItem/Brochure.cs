using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brochure : Interactable
{
    public UiBase uiBase;
    [SerializeField] private BlinkingObject blinkingObject;

    protected override void Interact()
    {
        UiManager.instance.CheckUi(uiBase);

        if (blinkingObject != null)
            blinkingObject.StopBlinking();

        //if (!uiBase.isOpen)
        //{
        //    UiManager.instance.CheckUi(uiBase);

        //    if (blinkingObject != null)
        //        blinkingObject.StopBlinking();
        //}


    }

    public void CloseUi()
    {
        UiManager.instance.CheckUi(uiBase);

    }
}
