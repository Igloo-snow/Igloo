using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UiManager uiManager;
    [SerializeField]
    private UiBase panel;

    private void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckUi();
        }
    }

    public void CheckUi()
    {
        uiManager.CheckUi(panel);
    }

    public void ClickExit()
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
