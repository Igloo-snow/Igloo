using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthDoor : Door
{
    void Start()
    {
        levers = new bool[3];
        animator = GetComponent<Animator>();
    }

    public override void CheckLevers()
    {
        isOpen = !(!levers[0] || levers[1]) && levers[2];
    }
}
