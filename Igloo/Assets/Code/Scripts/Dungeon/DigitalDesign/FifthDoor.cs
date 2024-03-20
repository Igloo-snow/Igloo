using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthDoor : Door
{
    public GameObject itemPrefab;
    public GameObject answerFXPrefab;
    private bool isFirst = true;

    void Start()
    {
        levers = new bool[4];
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
                Instantiate(answerFXPrefab);
                isFirst = false;
            }
        }
    }

    public override void CheckLevers()
    {
        isOpen = (levers[0] || !levers[1]) && !levers[2] && (levers[2] || levers[3]);
    }
}
