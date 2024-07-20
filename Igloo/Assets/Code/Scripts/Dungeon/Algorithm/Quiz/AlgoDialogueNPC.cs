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
        //��� ��縦 ������ ��
        dialogue.dialogues = AlgoDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        
        return dialogue.dialogues;
    }

    public void ShowDialogue()
    {
        AlgoSpeechBubbleManager.instance.TryStartDialogue(dialogue);
    }

}
