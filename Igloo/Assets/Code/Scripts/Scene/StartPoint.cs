using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private ActionController player;
    [SerializeField]
    [Tooltip("이전 씬 이름 + 시작 위치 설정한 오브젝트를 넣으세요")]
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
