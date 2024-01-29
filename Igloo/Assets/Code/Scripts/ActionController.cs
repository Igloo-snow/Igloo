using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    private CinemachineFreeLook freeLook;

    public bool encountered = false;
    public bool changingScene;
    public string nextScene;

    private void Start()
    {
        freeLook = FindObjectOfType<CinemachineFreeLook>(); 
    }

    void Update()
    {
        if (QuestUI.isCheckingQuest)
        {
            freeLook.enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            freeLook.enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }

        if(encountered == true && Input.GetKeyDown(KeyCode.E))
        {
            changingScene = true;
        }
        else
        {
            changingScene = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("TransitionPoint"))
        {
            encountered = true;
            nextScene = other.gameObject.GetComponent<TransitionPoint>().nextScene;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TransitionPoint"))
        {
            encountered = false;
        }
    }

    public TransitionPoint GetInteractableObject()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out TransitionPoint transitionInteractable)){
                return transitionInteractable;
            }
        }
        return null;
    }


}
