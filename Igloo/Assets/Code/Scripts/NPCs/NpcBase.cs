using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcBase : MonoBehaviour
{
    protected Animator anim;
    public LayerMask layerMask;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerEnter();
    }

    protected virtual void PlayerEnter()
    {
        if(Physics.CheckSphere(transform.position, 3f, layerMask))
        {
            anim.SetTrigger("Interact");
        }
    }


}