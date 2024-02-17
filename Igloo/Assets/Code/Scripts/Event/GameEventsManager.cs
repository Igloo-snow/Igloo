using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {  get; private set; }

    public QuestEvents questEvents;
    public PlayerEvents playerEvents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            questEvents = new QuestEvents();
            playerEvents = new PlayerEvents();
        }
        else
        {
            Destroy(gameObject);
        }
  
    }
}
