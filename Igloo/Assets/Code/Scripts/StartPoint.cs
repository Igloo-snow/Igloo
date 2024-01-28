using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;
    [SerializeField]
    [Tooltip("���� �� �̸� + ���� ��ġ ������ ������Ʈ�� ��������")]
    private GameObject[] startPoints;
    private string previousScene;

    private void Start()
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
                actionController.GetComponent<CharacterController>().enabled = true;
                return;
            }
        }
        Debug.Log("��ġ�ϴ� ���� ��ġ ����");
    }
}
