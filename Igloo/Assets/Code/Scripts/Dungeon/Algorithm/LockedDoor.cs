using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LockedDoor : Interactable
{
    [SerializeField] Item keyItem; 

    public bool isOpen;
    private Animator animator;

    void Start()
    {
        promptMessage = "";
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
            promptMessage = "";

    }

    protected override void Interact()
    {
        if (!InventoryManager.Instance.CheckInventory(keyItem.id))
        {
            promptMessage = "문이 잠겨있다";
            return;
        }
        else
        {
            promptMessage = "";
        }
        
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetTrigger("open");
            SoundManager.instance.Play("OpenWoodDoor3");

        }
    }
}
