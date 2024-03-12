using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Npc : MonoBehaviour
{
    [HideInInspector]
    public int npcId;
    private DialogueTrigger dialogueTrigger;
    private QuestPoint questPoint;
    private string questId; 
    protected Animator anim;

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;

        GameEventsManager.instance.dialogueEvents.onStartDialogue += StartDialogue;


    }
    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEventsManager.instance.dialogueEvents.onStartDialogue -= StartDialogue;

    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        dialogueTrigger = GetComponentInChildren<DialogueTrigger>();
        questPoint = GetComponentInChildren<QuestPoint>();
    }

    private void Start()
    {
        npcId = dialogueTrigger.npcId;
        if(questPoint != null )
        {
            questId = questPoint.questId;

        }
    }

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (Data.instance.npcData.ContainsKey(npcId))
    //    {
    //        Debug.Log(npcId + ":" + dialogueTrigger.index + "----" + Data.instance.npcData[npcId]);
    //        dialogueTrigger.index = Data.instance.npcData[npcId];
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.Play("muffledtalkingShort", Sound.Effect, 1.4f);
            anim.SetTrigger("Interact");
        }
    }

    private void StartDialogue(int id)
    {
        if(id == npcId)
        {
            foreach(npcData npc in DataManager.instance.nowPlayer.npcs)
            {
                if(npc.npcId == id)
                {
                    dialogueTrigger.index = npc.index;
                    Debug.Log(npc.npcId + " npc 스크립트 안" + npc.index);
                    return;
                }
            }
            DataManager.instance.nowPlayer.npcs.Add(new npcData { npcId = id, index = dialogueTrigger.index });
        }
    }

}
