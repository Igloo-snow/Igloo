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
    private ChangeSceneOnTimer timer;

    void Start()
    {
        promptMessage = "";
        timeline = GetComponent<PlayableDirector>();
        EndCredits.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        timer = GetComponent<ChangeSceneOnTimer>();

        timer.enabled = false;
    }

    protected override void Interact()
    {
        timeline.Play();
        SoundManager.instance.Play("resolution", Sound.Bgm, 1f, 0.3f);
        EndCredits.SetActive(true);
        timer.enabled = true;
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
