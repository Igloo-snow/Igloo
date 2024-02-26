using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : Door
{
    void Start()
    {
        levers = new bool[1];
        animator = GetComponent<Animator>();
    }

    public override void CheckLevers()
    {
        isOpen = !levers[0];
    }
}
