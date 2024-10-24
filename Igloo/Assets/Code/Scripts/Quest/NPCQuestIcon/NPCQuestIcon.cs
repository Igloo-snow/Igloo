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
        UpdateIcon(); // 처음 상태에 맞는 아이콘을 설정
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
                iconObject.SetActive(true); // 아이콘 표시
                break;

            case QuestState.IN_PROGRESS:
                iconRenderer.sprite = inProgressIcon;
                iconObject.SetActive(true); // 아이콘 표시
                break;

            case QuestState.CAN_FINISH:
                iconRenderer.sprite = canFinishIcon;
                iconObject.SetActive(true); // 아이콘 표시
                break;

            case QuestState.FINISHED:
                iconObject.SetActive(false); // 아이콘 숨기기
                break;

            default:
                iconObject.SetActive(false); // 기본적으로 아이콘 숨기기
                break;
        }
    }
}