using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveInteractable : Interactable
{
    protected override void Interact()
    {
        QuestManager.instance.SaveQuestData();

        DataManager.instance.SaveData();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
