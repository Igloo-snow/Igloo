using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool[] levers;
    protected Animator animator;
    protected bool isOpen;

    void Start()
    {
        //levers = new bool[1];
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpen)
        {
            animator.SetTrigger("open");
        }
    }

    public virtual void CheckLevers()
    {

    }

    public void SetLevers(bool lever, int index)
    {
        levers[index] = lever;
    }
}
