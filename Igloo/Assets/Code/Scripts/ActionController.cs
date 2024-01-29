using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    public bool encountered = false;
    public bool changingScene;
    public string nextScene;

    private void Start()
    {
    }

    void Update()
    {
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


}
