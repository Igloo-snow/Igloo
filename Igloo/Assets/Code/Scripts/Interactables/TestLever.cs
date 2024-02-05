using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLever : Interactable
{
    public bool leverValue = false;
    private Animator animator;
    [SerializeField]
    private TestDoor testDoor;

    void Start()
    {
        promptMessage = "False";
        animator = GetComponent<Animator>();
        testDoor = FindObjectOfType<TestDoor>();
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
        testDoor.SetLevers(leverValue, 0);
        testDoor.CheckLevers();
    }

    public bool GetLeverValue()
    {
        return leverValue;
    }
}
