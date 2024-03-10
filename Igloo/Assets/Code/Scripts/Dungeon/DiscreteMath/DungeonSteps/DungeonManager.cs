using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private List<DungeonStepBase> steps;

    private DungeonStepBase currentTutorial = null;
    private int currentIndex = -1;

    private void Start()
    {
        SetNextTutorial();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        if (currentIndex >= steps.Count - 1)
        {
            Debug.Log("in settexttutorial");
            CompletedAllTutorials();
            return;
        }

        currentIndex++;
        currentTutorial = steps[currentIndex];

        currentTutorial.Enter();
    }

    private void CompletedAllTutorials()
    {
        currentTutorial = null;

        // 던전 종료 시 수행할 행동
        Debug.Log("Complete All");

    }


}