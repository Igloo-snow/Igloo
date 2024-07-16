using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private List<DialogueStepBase> DialogueSteps;
    public CanvasGroup canvasGroup;

    private DialogueStepBase currentDialogue = null;
    private int currentIndex = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UI 켜기
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

            //대화 진행
            SetNextDialogue();

        }
    }

    private void Update()
    {
        if (currentDialogue != null)
        {
            currentDialogue.Execute(this);
        }
    }

    public void SetNextDialogue()
    {
        if (currentDialogue != null)
        {
            currentDialogue.Exit();
        }

        if (currentIndex >= DialogueSteps.Count - 1)
        {
            Debug.Log("in settexttutorial");
            CompletedAll();
            return;
        }

        currentIndex++;
        currentDialogue = DialogueSteps[currentIndex];

        currentDialogue.Enter();
    }

    private void CompletedAll()
    {
        currentDialogue = null;

        // 퀴즈 UI 종료
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
