using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWNpc : MonoBehaviour
{
    public GameObject speechBubble;
    protected Animator anim;
    public LayerMask layerMask;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Interact");
            speechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speechBubble.SetActive(false);
        }
    }
}
