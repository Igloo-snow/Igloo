using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    ElevatorButton button;

    private Animator animator;

    private bool isAtUpperFloor = false;
    private bool isMoving = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ButtonStepped()
    {
        if (!isMoving)
        {
            if (!isAtUpperFloor)
            {
                animator.SetTrigger("IsGoingUp");
                isAtUpperFloor = true;
            }
            else
            {
                animator.SetTrigger("IsGoingDown");
                isAtUpperFloor = false;
            }
        }
    }

    public void CheckMoving()
    {
        isMoving = !isMoving;
    }

}
