using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoor : Door
{
    void Start()
    {
        levers = new bool[2];
        animator = GetComponent<Animator>();
    }

    public override void CheckLevers()
    {
        isOpen = levers[0] || levers[1];
    }
}
