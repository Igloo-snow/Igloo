using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    //퀘스트 시스템 확인을 위한 임시 변수
    public int coins = 0;

    public bool encountered = false;
    public bool changingScene;
    //public string previousScene;
    public string nextScene;

    //[SerializeField]
    //private GameObject[] startPoints;

    private void Start()
    {
        //SetPlayerPos(SceneMgr.instance.previousScene) ;
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

        if(Input.GetKeyDown(KeyCode.S))
        {
            coins++;
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

    /*
    public void SetPlayerPos(string previousScene)
    {

        Debug.Log(previousScene + SceneManager.GetActiveScene().name + startPoints.Length + "  test1");
        for (int i = 0; i < startPoints.Length; i++)
        {
            Debug.Log("startPoint test2");
            if (previousScene == startPoints[i].name)
            {
                Debug.Log("startPoint test3");

                GetComponent<CharacterController>().enabled = false;
                transform.position = startPoints[i].transform.position;
                GetComponent<CharacterController>().enabled = true;
                break;
            }
        }

    }
    */

}
