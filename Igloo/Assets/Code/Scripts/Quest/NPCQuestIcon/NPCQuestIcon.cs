using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestIcon : MonoBehaviour
{
    [Header("Quest Icon Sprites")]
    [SerializeField] private Sprite canStartIcon;
    [SerializeField] private Sprite inProgressIcon;
    [SerializeField] private Sprite canFinishIcon;

    [Header("Icon Settings")]
    [SerializeField] private GameObject iconObject;
    private SpriteRenderer iconRenderer;

    private QuestManager questManager;
    private string questId;
    private QuestState currentQuestState;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        iconRenderer = iconObject.GetComponent<SpriteRenderer>();
        QuestPoint questPoint = GetComponent<QuestPoint>();
        questId = questPoint.questId;
        UpdateIcon(); // ó�� ���¿� �´� �������� ����
    }

    private void Update()
    {
        Quest quest = questManager.GetQuestById(questId);
        if (quest != null)
        {
            currentQuestState = quest.state;
            UpdateIcon();
        }
    }

    private void UpdateIcon()
    {
        switch (currentQuestState)
        {
            case QuestState.CAN_START:
                iconRenderer.sprite = canStartIcon;
                iconObject.SetActive(true); // ������ ǥ��
                break;

            case QuestState.IN_PROGRESS:
                iconRenderer.sprite = inProgressIcon;
                iconObject.SetActive(true); // ������ ǥ��
                break;

            case QuestState.CAN_FINISH:
                iconRenderer.sprite = canFinishIcon;
                iconObject.SetActive(true); // ������ ǥ��
                break;

            case QuestState.FINISHED:
                iconObject.SetActive(false); // ������ �����
                break;

            default:
                iconObject.SetActive(false); // �⺻������ ������ �����
                break;
        }
    }
}