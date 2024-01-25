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
    private PlayerMovement player;

    private Canvas canvas;

    //�̵�Ȯ�� UI
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
        canvas = FindObjectOfType<Canvas>();
        // ���������κ��� �ν��Ͻ� ����
        moveConfirmationUIInstance = Instantiate(moveConfirmationUIPrefab, canvas.transform);
        //questUIInstance = Instantiate(questUIPrefab, canvas.transform);
        // �ʱ� ����
        moveConfirmationUIInstance.SetActive(false);

        actionController = FindObjectOfType<ActionController>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ο� ���� �ε�� �� �ʱ�ȭ �ڵ� ����
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
