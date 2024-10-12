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
        promptMessage = "E : �����̿��� ���ɱ�";
    }

    private AlgoDialogue[] GetDialogue()
    {
        //��� ��縦 ������ ��
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
