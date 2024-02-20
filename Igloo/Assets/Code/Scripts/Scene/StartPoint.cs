using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private ActionController player;
    [SerializeField]
    [Tooltip("���� �� �̸� + ���� ��ġ ������ ������Ʈ�� ��������")]
    public List<GameObject> startPointsList;
    private string previousScene;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            previousScene = PlayerPrefs.GetString("PreviousScene");
            SetPlayerPos();
        }
    }

    private void SetPlayerPos()
    {
        foreach(GameObject obj in startPointsList)
        {
            if (obj.name.Equals(previousScene))
                player.Reposition(obj);
        }
    }
}
