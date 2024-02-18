using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTextBookStep : QuestStep
{
    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onFinishDungeon += FinishDungeon;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onFinishDungeon -= FinishDungeon;
    }

    private void FinishDungeon(string name)
    {
        if (name.Equals(targetScene))
        {
            FinishQuestStep();
        }
    }
}
