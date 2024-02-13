using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : Door
{
   /* public bool[] levers;
    private Animator animator;
    private bool isOpen;*/

    void Start()
    {
        levers = new bool[1];
        animator = GetComponent<Animator>();
    }

    public override void CheckLevers()
    {
        bool result = true;

        foreach (bool lever in levers)
        {
            result &= lever;
        }

        isOpen = result;
        Debug.Log("isOpen = " + isOpen);
    }
}
