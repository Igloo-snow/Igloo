using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueTrigger : Interactable
{
    private void Start()
    {
        promptMessage = "E : 눈송이에게 말걸기";
    }
    protected override void Interact()
    {
        GetComponent<AlgoDialogueNPC>().ShowDialogue();
        //이게 중복해서 눌릴 수도 있나?
    }
}
