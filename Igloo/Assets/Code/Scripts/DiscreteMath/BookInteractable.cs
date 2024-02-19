using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteractable : Interactable
{
    [SerializeField] private BookItem parent;

    protected override void Interact()
    {
        //this.gameObject.SetActive(false);
        parent.isFinish = true;
        promptMessage = "";
    }
}
