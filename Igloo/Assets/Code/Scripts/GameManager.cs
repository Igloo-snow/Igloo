using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static bool isPlay;
    //UI 관련 상태 
    public static bool isOpenQuestUI = false;
    public static bool isOpenDialogue = false;
    public static bool isOpenRestartUI = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenRestartUI || isOpenQuestUI)
        {
            GameEventsManager.instance.playerEvents.PlayerStop();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //isPlay = false;
        }
        else
        {
            GameEventsManager.instance.playerEvents.PlayerStart();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //isPlay = true;
        }
    }
}
