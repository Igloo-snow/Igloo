using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveInteractable : Interactable
{
    protected override void Interact()
    {
        DataManager.instance.SaveData();
    }
}
