using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : Interactable
{
    private Animator anim;
    [SerializeField] private GameObject item;
    [SerializeField] private BoxCollider triggerBox;

    private void Start()
    {
        promptMessage = "상자를 연다:E";
        anim = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        item.SetActive(true);
        promptMessage = "";
        anim.SetTrigger("Open");
        triggerBox.enabled = false;
    }
}
