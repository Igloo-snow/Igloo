using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnivDoor : Interactable
{
    public bool isOpen;
    private Animator animator;

    void Start()
    {
        promptMessage = "";
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetTrigger("open");
        }
        else
        {
            animator.SetTrigger("close");
        }
    }
}
