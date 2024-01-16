using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private ActionController actionController;

    public GameObject moveConfirmationUIPrefab;
    private GameObject moveConfirmationUIInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            actionController = FindObjectOfType<ActionController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (actionController.encountered)
        {
            ShowMoveConfirmationUI();
        }
        else
        {
            HideMoveConfirmationUI();
        }
    }

    void InitializeUI()
    {
        // 프리팹으로부터 인스턴스 생성
        moveConfirmationUIInstance = Instantiate(moveConfirmationUIPrefab);
        // 초기에는 비활성화
        moveConfirmationUIInstance.SetActive(false);

        actionController = FindObjectOfType<ActionController>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새로운 씬이 로드될 때 초기화 코드 실행
        InitializeUI();
    }

    public void ShowMoveConfirmationUI()
    {
        moveConfirmationUIInstance.SetActive(true);
        moveConfirmationUIInstance.GetComponentInChildren<TMP_Text>().text =" To move to "  + actionController.nextScene + ", Press the " + "<color=red>" + "E key" + "</color>";
    }

    public void HideMoveConfirmationUI()
    {
        moveConfirmationUIInstance.SetActive(false);
    }
}
