using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStepUIController : MonoBehaviour
{
    public GameObject questUI;
    public string targetQuestId = "TestQuest"; 
    public float interactionRange = 3f;

    private bool isUIActive = false; 
    private Transform player;  
    private QuestManager questManager; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        questManager = QuestManager.instance;  

        if (questUI != null)
        {
            questUI.SetActive(false);  
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange && IsQuestCompleted())
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                ToggleUI();
            }
        }
    }

    bool IsQuestCompleted()
    {
        Quest quest = questManager.GetQuestById(targetQuestId);

        if (quest != null && quest.state == QuestState.FINISHED)
        {
            return true;
        }
        return false;
    }

    void ToggleUI()
    {
        isUIActive = !isUIActive;
        if (questUI != null)
        {
            questUI.SetActive(isUIActive);
        }
    }
}