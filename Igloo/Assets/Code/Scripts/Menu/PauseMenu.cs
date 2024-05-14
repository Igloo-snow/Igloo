using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private UiManager uiManager;
    [SerializeField]
    private UiBase panel;
    [SerializeField]
    private GameObject keyGuide;

    private void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(keyGuide.activeSelf == true) 
            {
                Debug.Log("keyguide open");
                keyGuide.SetActive(false);
                panel.gameObject.SetActive(true);
            }
            else
            {
                CheckUi();
            }
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

        FindAnyObjectByType<SceneMgr>().StartLoadScene((int)SceneNames.MainMenu);
    }
}
