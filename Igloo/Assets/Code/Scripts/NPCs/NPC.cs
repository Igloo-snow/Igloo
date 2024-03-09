using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    protected Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.Play("muffledtalkingShort", Sound.Effect, 1.4f);
            anim.SetTrigger("Interact");
        }
    }

}
