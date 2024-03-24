using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndCreditsNPC : Interactable
{
    private PlayableDirector timeline;
    public GameObject EndCredits;
    public GameObject speechBubble;
    protected Animator anim;

    void Start()
    {
        promptMessage = "";
        timeline = GetComponent<PlayableDirector>();
        EndCredits.SetActive(false);
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Interact()
    {
        timeline.Play();
        EndCredits.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            anim.SetTrigger("Interact");
            speechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            speechBubble.SetActive(false);
        }
    }
}
