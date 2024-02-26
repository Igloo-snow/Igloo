using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthDoor : Door
{
    void Start()
    {
        levers = new bool[4];
        animator = GetComponent<Animator>();
    }

    public override void CheckLevers()
    {
        isOpen = (levers[0] || !levers[1]) && !levers[2] && (levers[2] || levers[3]);
    }
}
