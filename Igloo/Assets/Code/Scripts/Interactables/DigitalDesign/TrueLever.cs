using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueLever : Interactable
{
    public bool leverValue = true;
    private Animator animator;
    [SerializeField]
    private Door Door;
    [SerializeField]
    private int leverIndex;

    void Start()
    {
        promptMessage = "True";
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
