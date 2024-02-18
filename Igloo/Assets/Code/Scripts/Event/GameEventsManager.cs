using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {  get; private set; }

    public QuestEvents questEvents;
    public PlayerEvents playerEvents;
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            questEvents = new QuestEvents();
            playerEvents = new PlayerEvents();
            dialogueEvents = new DialogueEvents();
        }
        else
        {
            Destroy(gameObject);
        }
  
    }
}
