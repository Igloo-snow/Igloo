using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueNPC : MonoBehaviour
{
    [SerializeField] private AlgoDialogueOccasion dialogue;

    private void Start()
    {
        GetDialogue();
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

}
