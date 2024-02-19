using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionUI : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;
    //private PlayerMovement player;
    [SerializeField]
    private Canvas canvas;

    //이동확인 UI
    public GameObject moveConfirmationUIPrefab;
    private GameObject moveConfirmationUIInstance;


    void Awake()
    {
        InitializeUI();

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
        moveConfirmationUIInstance = Instantiate(moveConfirmationUIPrefab, canvas.transform);
        //questUIInstance = Instantiate(questUIPrefab, canvas.transform);
        // 초기 설정
        moveConfirmationUIInstance.SetActive(false);
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
