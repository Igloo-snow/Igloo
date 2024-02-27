using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private Animator anim;
    private int dialogueCount = 0;
    [SerializeField] private int maxCount = 1;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    private bool playerIsNear = false;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onFinishDialogue += FinishDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onFinishDialogue -= FinishDialogue;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void FinishDialogue(int id)
    {
        // �� �κ��� �ش� npc�� ��ũ��Ʈ�� �� �پ��־ ȣ���� �ż� ������ �����
        //if (dialogueTrigger.dialogue.dialogueId.Equals(id) && playerIsNear)
        //{
        //    dialogueCount++;
        //    if (maxCount >= dialogueCount)
        //        dialogueCount = maxCount;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Interact");
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
