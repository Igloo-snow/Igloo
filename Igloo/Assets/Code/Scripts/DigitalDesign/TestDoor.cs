using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    public bool[] levers;
    private Animator animator;
    private bool isOpen;

    void Start()
    {
        levers = new bool[1];
        //levers[0] = FindObjectOfType<TestLever>().getLeverValue();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOpen)
        {
            Debug.Log(isOpen);
            animator.SetTrigger("open");
        }
    }

    public void CheckLevers()
    {
        bool result = true;

        foreach (bool lever in levers)
        {
            result &= lever;
        }

        isOpen = result;
        Debug.Log("isOpen = " + isOpen);
    }

    public void SetLevers (bool lever, int index)
    {
        levers[index] = lever;
    }
}
