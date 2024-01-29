using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook[] freelooks;
    [SerializeField]
    private ActionController actionController;
    [SerializeField]
    [Tooltip("이전 씬 이름 + 시작 위치 설정한 오브젝트를 넣으세요")]
    private GameObject[] startPoints;
    private string previousScene;

    private void Awake()
    {
        SetPlayerPos();
    }

    private void SetPlayerPos()
    {
        previousScene = SceneMgr.instance.previousScene;
        for (int i = 0; i < startPoints.Length; i++)
        {
            if (previousScene.Equals(startPoints[i].name))
            {
                actionController.GetComponent<CharacterController>().enabled = false;
                actionController.transform.position = startPoints[i].transform.position;
                actionController.transform.forward = startPoints[i].transform.forward;
                freelooks[i].LookAt = actionController.transform;
                freelooks[i].Follow = actionController.transform;
                freelooks[i].Priority++;


                actionController.GetComponent<CharacterController>().enabled = true;
                return;
            }
            else
            {
                freelooks[i].gameObject.SetActive(false);
            }
        }
    }
}
