using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TutorialQuestStart : TutorialBase
{
    [SerializeField] private BlinkingObject effectPrefab;
    private BlinkingObject effect;
    [SerializeField] private QuestInfoSO relatedQuest;
    [SerializeField] private GameObject questInfoPrefab;
    private GameObject questInfo;
    private bool isCompleted = false;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCompleted = true;
            if(questInfo != null)
            {
                Destroy(questInfo);
            }
        }
    }

    private void StartQuest(string id)
    {
        if (relatedQuest.id.Equals(id))
        {
            questInfo = Instantiate(questInfoPrefab, FindAnyObjectByType<Canvas>().transform);

        }
    }

    public override void Enter()
    {
        effect = Instantiate(effectPrefab, this.transform);
        effect.StartBlinking();
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted)
        {
            effect.StopBlinking();
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Destroy(questInfo);

    }
}
