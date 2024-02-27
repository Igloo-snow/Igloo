using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthDoor : Door
{
    public GameObject itemPrefab;
    private bool isFirst = true;

    void Start()
    {
        levers = new bool[3];
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpen)
        {
            animator.SetTrigger("open");

            if (isFirst)
            {
                GameObject instance = Instantiate(itemPrefab);
                isFirst = false;
            }
        }
    }

    public override void CheckLevers()
    {
        isOpen = !(!levers[0] || levers[1]) && levers[2];
    }
}
