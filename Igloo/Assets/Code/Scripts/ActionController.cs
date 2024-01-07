using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public static ActionController instance;

    public bool encountered = false;
    public bool changingScene;
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(encountered == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("cs true");
            changingScene = true;
        }
        else
        {
            changingScene = false;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("TransitionPoint"))
        {
            encountered = true;
            nextScene = collision.gameObject.name;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("TransitionPoint"))
        {
            encountered = true;
            nextScene = other.gameObject.name;
        }
    }*/
    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("TransitionPoint"))
        {
            encountered = false;
        }
    }*/


}
