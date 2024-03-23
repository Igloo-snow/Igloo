using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepController : MonoBehaviour
{
    public static StepController instance;
    [SerializeField] private List<StepBase> steps;


    private StepBase currentStep = null;
    private int currentIndex = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetNextStep();
    }

    private void Update()
    {
        if (currentStep != null)
        {
            currentStep.Execute(this);
        }
    }

    public void SetNextStep()
    {
        if (currentStep != null)
        {
            currentStep.Exit();
        }

        if (currentIndex >= steps.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        currentIndex++;
        currentStep = steps[currentIndex];

        currentStep.Enter();
    }

    private void CompletedAllTutorials()
    {
        currentStep = null;

        // 튜토리얼 종료 시 수행할 행동
        Debug.Log("Complete All in StepController");

    }


}
