using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLever : Interactable
{
    public bool leverValue = false;
    private Animator animator;
    [SerializeField]
    private Door Door;
    [SerializeField]
    private int leverIndex;

    void Start()
    {
        promptMessage = "False";
        animator = GetComponent<Animator>();
        //Door = FindObjectOfType<Door>();

        string name = gameObject.name.Substring(13, 1);
        leverIndex = int.Parse(name);
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
            animator.SetTrigger("up");
        }
        else
        {
            promptMessage = "False";
            animator.SetTrigger("down");
        }
        Door.SetLevers(leverValue, leverIndex);
        Door.CheckLevers();
    }

}
