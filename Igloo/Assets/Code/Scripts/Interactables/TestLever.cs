using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLever : Interactable
{
    bool leverValue = false;
    private Animator animator;

    void Start()
    {
        promptMessage = "False";
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        leverValue = !leverValue;
        if (leverValue)
        {
            promptMessage = "True";
            Debug.Log("now true");
            //GetComponent<Animator>().Play("PullUp");
            animator.SetTrigger("up");
        }
        else
        {
            promptMessage = "False";
            Debug.Log("now false");
            //GetComponent<Animator>().Play("PullDown");
            animator.SetTrigger("down");
        }
    }

    public bool getLeverValue()
    {
        return leverValue;
    }
}
