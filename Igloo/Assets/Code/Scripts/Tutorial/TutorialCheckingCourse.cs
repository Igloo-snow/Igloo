using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheckingCourse : TutorialBase
{
    [SerializeField] private GameObject brochure;
    [SerializeField] private Image brochureUi;
    [SerializeField] private TMP_Text infoUi; 
    private bool isCompleted = false;

    public override void Enter()
    {
        Invoke("DropBrochure", 3f);
    }

    private void DropBrochure()
    {
        brochure.SetActive(true);

    }

    public override void Execute(TutorialController controller)
    {
        if(isCompleted)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
    }

    public void CloseBrochure()
    {
        brochure.SetActive(false);
        infoUi.text = "";
        //isCompleted = true;

    }

}
