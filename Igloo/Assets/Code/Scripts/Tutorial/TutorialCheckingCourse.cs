using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheckingCourse : TutorialBase
{
    [SerializeField] private GameObject brochure;
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
    }

    public override void Exit()
    {
    }

}
