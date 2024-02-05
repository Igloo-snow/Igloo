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

    public int health = 3;

    private void Start()
    {
        freeLook = FindObjectOfType<CinemachineFreeLook>();
    }

    void Update()
    {
        if (QuestUI.isCheckingQuest || DialogueManager.GetInstance().isPlaying)
        {
            //freeLook.enabled = false;
            //GetComponent<CharacterController>().enabled = false;
            Time.timeScale = 0f;
        }
        else
        {
            //freeLook.enabled = true;
            //GetComponent<CharacterController>().enabled = true;
            Time.timeScale = 1.0f;
        }

        if(encountered == true && Input.GetKeyDown(KeyCode.E))
        {
            changingScene = true;
        }
        else
        {
            changingScene = false;
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

    public void Reposition(Vector3 pos)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = pos;
        GetComponent<CharacterController>().enabled = true;
    }

    public void HealthDown()
    {
        if(health - 1 <= 0)
        {
            PlayerDie();
        }
        else
        {
            health--;
        }
    }

    public void PlayerDie()
    {
        GameEventsManager.instance.playerEvents.PlayerDie();
        // 죽는 효과 추가
    }
}
