using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //UI 관련 상태 
    public static bool isStaticUiOpen = false;
    public static bool isDynamicUiOpen = false;
    public static bool isOpenUI = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStaticUiOpen)
        {
            GameEventsManager.instance.playerEvents.PlayerStop();
        }
        else
        {
            GameEventsManager.instance.playerEvents.PlayerStart();
        }
    }
}
