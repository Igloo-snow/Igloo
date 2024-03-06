using UnityEngine;
using UnityEngine.UI;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField]
    private FadeEffect fadeEffect;
    [SerializeField]
    private Image fadeUi;
    [SerializeField]
    private bool isFadeIn = false;
    private bool isCompleted = false;

    public override void Enter()
    {
        fadeUi.gameObject.SetActive(false);
        isCompleted = true;
        //if (isFadeIn == true)
        //{
        //    fadeEffect.FadeIn(OnAfterFadeEffect);
        //}
        //else
        //{
        //    fadeEffect.FadeOut(OnAfterFadeEffect);
        //}
    }

    private void OnStayHold()
    {

    }

    private void OnAfterFadeEffect()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }
}

