using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Teleportation : Interactable
{
    private Animator animator;
    private ActionController player;


    private RaycastHit[] hit;
    [SerializeField] GameObject destination;
    private Vector3 detectPlayerArea;

    private bool isAnimationPlaying = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<ActionController>();

        detectPlayerArea = transform.position + new Vector3(0, 2, 0); 
    }

    private void Update()
    {
        hit = Physics.SphereCastAll(detectPlayerArea, 2.5f, Vector3.up, 0);
        foreach(RaycastHit hit in hit)
        {
            if (hit.transform.CompareTag("Player"))
            {
                if (!isAnimationPlaying)
                {
                    animator.SetBool("NearPlayer", true);
                    isAnimationPlaying = true;
                }
            }
            else
            {
                if (isAnimationPlaying)
                {
                    animator.SetBool("NearPlayer", false);
                    isAnimationPlaying = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPlayerArea, 2.5f);
    }

    protected override void Interact()
    {
        if (destination)
        {
            player.Reposition(destination);
        }
    }
}
