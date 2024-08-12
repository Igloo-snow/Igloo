using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class OneSideDoor : Interactable
{
    private Animator animator;
    [SerializeField] DoorOpenTrigger doorOpen;
    private int count = 1;

    private void Start()
    {
        animator = GetComponent<Animator>();
        promptMessage = "이쪽에서는 열리지 않는다";
    }

    private void Update()
    {
        if (count > 0 && doorOpen.canOpen)
        {
            promptMessage = "";
        }
    }

    protected override void Interact()
    {
        if (doorOpen.canOpen && count > 0)
        {
            animator.SetTrigger("open");
            SoundManager.instance.Play("OpenWoodDoor3");
            count--;
        }


    }
}
