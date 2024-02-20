using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Interact");
        }

    }
}
