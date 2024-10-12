using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlgoNpc : Interactable
{
    [SerializeField] private AlgoDialogueOccasion dialogue;
    [SerializeField] bool speechBubbleRelated = false;

    private void OnEnable()
    {
        GameEventsManager.instance.algoEvents.onAlgoQuizRight += StartActing;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.algoEvents.onAlgoQuizRight -= StartActing;
    }

    private void Start()
    {
        GetDialogue();
        promptMessage = "E : 눈송이에게 말걸기";
    }

    private AlgoDialogue[] GetDialogue()
    {
        //모든 대사를 가져온 것
        dialogue.dialogues = AlgoDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        
        return dialogue.dialogues;
    }

    public void ShowDialogue()
    {
        AlgoSpeechBubbleManager.instance.TryStartDialogue(dialogue);
    }

    protected override void Interact()
    {
        GetComponent<AlgoNpc>().ShowDialogue();
    }

    private void StartActing(int id, int eventId)
    {
        if(id == dialogue.npcId)
        {
            if (GetComponent<AlgoNpcAction>())
            {
                GetComponent<AlgoNpcAction>().nextEventId = eventId;
                GetComponent<AlgoNpcAction>().Base();
            }
        }
    }


}
