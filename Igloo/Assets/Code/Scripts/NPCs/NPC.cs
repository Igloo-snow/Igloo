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
        questId = questPoint.questId;
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
            Debug.Log(id + "NPC 스크립트 안");
            if(!Data.instance.npcData.ContainsKey(id))
                Data.instance.npcData.Add(npcId, dialogueTrigger.index);
            else
            {
                dialogueTrigger.index = Data.instance.npcData[npcId];
            }
        }
    }

}
