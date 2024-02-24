using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TutorialQuestStart : TutorialBase
{
    [SerializeField] private BlinkingObject effectPrefab;
    private BlinkingObject effect;
    [SerializeField] private QuestInfoSO relatedQuest;
    private bool isCompleted = false;


    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;

    }

    private void StartQuest(string id)
    {
        if (relatedQuest.id.Equals(id))
        {
            isCompleted = true;
            Debug.Log("iscomplete true");
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

    }
}
