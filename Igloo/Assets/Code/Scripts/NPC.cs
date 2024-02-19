using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject beforeNpc;
    [SerializeField] private GameObject afterNpc;

    private QuestPoint questPoint;


    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvancdQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvancdQuest;
    }

    private void Start()
    {
        questPoint = GetComponentInChildren<QuestPoint>();
    }

    private void AdvancdQuest(string questName)
    {
        if(questPoint.questId.Equals(questName))
        {
            //���̾�α� ��������δ� �Ǵµ� ����Ʈ�� ������ �ȵǴµ�?
            beforeNpc.SetActive(false);
            afterNpc.SetActive(true);
        }
    }



}
