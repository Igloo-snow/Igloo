using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    private ActionController actionController;
    [SerializeField]
    [Tooltip("이전 씬 이름 + 시작 위치 설정한 오브젝트를 넣으세요")]
    private GameObject[] startPoints;
    private string previousScene;

    private void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        SetPlayerPos();
    }

    public void SetPlayerPos()
    {
        previousScene = SceneMgr.instance.previousScene;
        //Debug.Log(previousScene + SceneManager.GetActiveScene().name + startPoints.Length + "  test1");
        for (int i = 0; i < startPoints.Length; i++)
        {
            //Debug.Log(startPoints[i].gameObject.name + "startPoint test2" + previousScene);
            if (previousScene == startPoints[i].name)
            {
                Debug.Log("startPoint test3");

                actionController.GetComponent<CharacterController>().enabled = false;
                actionController.transform.position = startPoints[i].transform.position;
                actionController.GetComponent<CharacterController>().enabled = true;
                break;
            }
        }

    }
}
