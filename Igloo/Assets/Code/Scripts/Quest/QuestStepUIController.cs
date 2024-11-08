using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStepUIController : MonoBehaviour
{
    public GameObject questUI;  // 퀘스트 UI 오브젝트
    public string targetQuestId = "TestQuest";  // 완료 여부를 확인할 퀘스트 ID
    public float interactionRange = 3f;  // 노트북과의 상호작용 범위

    private bool isUIActive = false;  // UI 활성화 상태
    private Transform player;  // 플레이어의 트랜스폼
    private QuestManager questManager;  // 퀘스트 매니저

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // 플레이어를 찾습니다.
        questManager = QuestManager.instance;  // QuestManager를 찾습니다.

        if (questUI != null)
        {
            questUI.SetActive(false);  // 게임 시작 시 UI는 비활성화 상태로 설정
        }
    }

    void Update()
    {
        // 플레이어와 노트북의 거리 계산
        float distance = Vector3.Distance(player.position, transform.position);

        // 퀘스트가 완료되었고, 플레이어가 노트북과 가까운 범위 내에 있으면 E 키로 UI를 켤 수 있습니다.
        if (distance <= interactionRange && IsQuestCompleted())
        {
            if (Input.GetKeyDown(KeyCode.E))  // E 키를 누르면 UI를 토글
            {
                ToggleUI();
            }
        }
    }

    // 퀘스트가 완료되었는지 체크하는 함수
    bool IsQuestCompleted()
    {
        Quest quest = questManager.GetQuestById(targetQuestId);

        // 퀘스트가 존재하고, 완료 상태인지를 체크
        if (quest != null && quest.state == QuestState.FINISHED)
        {
            return true;
        }
        return false;
    }

    void ToggleUI()
    {
        // UI 활성화/비활성화 토글
        isUIActive = !isUIActive;
        if (questUI != null)
        {
            questUI.SetActive(isUIActive);
        }
    }
}