using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStepUIController : MonoBehaviour
{
    public GameObject questUI;  // ����Ʈ UI ������Ʈ
    public string targetQuestId = "TestQuest";  // �Ϸ� ���θ� Ȯ���� ����Ʈ ID
    public float interactionRange = 3f;  // ��Ʈ�ϰ��� ��ȣ�ۿ� ����

    private bool isUIActive = false;  // UI Ȱ��ȭ ����
    private Transform player;  // �÷��̾��� Ʈ������
    private QuestManager questManager;  // ����Ʈ �Ŵ���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // �÷��̾ ã���ϴ�.
        questManager = QuestManager.instance;  // QuestManager�� ã���ϴ�.

        if (questUI != null)
        {
            questUI.SetActive(false);  // ���� ���� �� UI�� ��Ȱ��ȭ ���·� ����
        }
    }

    void Update()
    {
        // �÷��̾�� ��Ʈ���� �Ÿ� ���
        float distance = Vector3.Distance(player.position, transform.position);

        // ����Ʈ�� �Ϸ�Ǿ���, �÷��̾ ��Ʈ�ϰ� ����� ���� ���� ������ E Ű�� UI�� �� �� �ֽ��ϴ�.
        if (distance <= interactionRange && IsQuestCompleted())
        {
            if (Input.GetKeyDown(KeyCode.E))  // E Ű�� ������ UI�� ���
            {
                ToggleUI();
            }
        }
    }

    // ����Ʈ�� �Ϸ�Ǿ����� üũ�ϴ� �Լ�
    bool IsQuestCompleted()
    {
        Quest quest = questManager.GetQuestById(targetQuestId);

        // ����Ʈ�� �����ϰ�, �Ϸ� ���������� üũ
        if (quest != null && quest.state == QuestState.FINISHED)
        {
            return true;
        }
        return false;
    }

    void ToggleUI()
    {
        // UI Ȱ��ȭ/��Ȱ��ȭ ���
        isUIActive = !isUIActive;
        if (questUI != null)
        {
            questUI.SetActive(isUIActive);
        }
    }
}