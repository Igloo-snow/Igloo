using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartNode : MonoBehaviour, IPointerClickHandler
{
    private int startNode;
    private GameObject clickedObject;
    public GM stageManager;
    public GameObject container;
    public GameObject[] stages;
    public TMP_Text title;
    public TMP_Text text;

    public void Start()
    {
        container.SetActive(false);
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].gameObject.SetActive(false);
        }
    }

    public void InitUi()
    {
        Debug.Log("inside startnode");
        StartCoroutine(InitUiCoroutine());
    }

    public IEnumerator InitUiCoroutine()
    {
        Debug.Log("inside coroutine");

        container.SetActive(true);
        title.text = "Stage " + (stageManager.currentStage + 1);

        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = container.GetComponent<Image>().color;
            c.a = f;
            container.GetComponent<Image>().color = c;
            yield return null;
        }
        GameManager.isOpenUI = true;
        title.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        stages[stageManager.currentStage-1].gameObject.SetActive(true);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickedObject = eventData.pointerCurrentRaycast.gameObject;
        startNode = Convert.ToInt32(clickedObject.name);
        Debug.Log(startNode);
        SetStartNode();
    }

    private void SetStartNode()
    {
        stageManager.SetStartPoint(startNode);
        container.SetActive(false);
        stages[stageManager.currentStage-1].SetActive(false);
        stageManager.RestartLevel();
        GameManager.isOpenUI = false;


    }
}
