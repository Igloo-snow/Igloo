using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueTrigger : MonoBehaviour
{
    [SerializeField] private AlgoDialogueOccasion dialogue;

    public AlgoDialogue[] GetDialogue()
    {
        //모든 대사를 가져온 것
        dialogue.dialogues = AlgoDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        
        return dialogue.dialogues;
    }

    private void Start()
    {
        GetDialogue();
    }
}
