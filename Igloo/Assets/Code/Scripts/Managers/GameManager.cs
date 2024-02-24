using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UI 관련 상태 
    public static bool isOpenQuestUI = false;
    public static bool isOpenDialogue = false;
    public static bool isOpenRestartUI = false;
    public static bool isOpenInfoUI = false;
    public static bool isOpenUI = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("DiscreteMath"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenRestartUI || isOpenQuestUI || isOpenInfoUI || isOpenUI)
        {
            GameEventsManager.instance.playerEvents.PlayerStop();
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            //isPlay = false;
        }
        else
        {
            GameEventsManager.instance.playerEvents.PlayerStart();
            //Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;
            //isPlay = true;
        }

    }
}
