using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueTrigger : Interactable
{
    private void Start()
    {
        promptMessage = "E : �����̿��� ���ɱ�";
    }
    protected override void Interact()
    {
        GetComponent<AlgoDialogueNPC>().ShowDialogue();
        //�̰� �ߺ��ؼ� ���� ���� �ֳ�?
    }
}
