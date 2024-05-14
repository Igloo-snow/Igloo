using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheckingCourse : TutorialBase
{
    [SerializeField] private GameObject brochure;
    [SerializeField] private GameObject brochureOnDisplay;
    [SerializeField] private TMP_Text infoUi; 
    private bool isCompleted = false;
    private GameObject nowBrochure;

    public override void Enter()
    {
        Invoke("DropBrochure", 0.8f);
    }

    private void DropBrochure()
    {
        brochure.SetActive(true);
        nowBrochure = brochure;

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
        infoUi.text = "";
    }

    public void CloseBrochure()
    {
        if(nowBrochure == brochure)
        {
            StartCoroutine("DestroyFirstBrochure");
            infoUi.text = "";
            brochureOnDisplay.SetActive(true);
            nowBrochure = brochureOnDisplay;
        }
        else
        {
            isCompleted = true;
        }

    }

    IEnumerator DestroyFirstBrochure()
    {
        brochure.transform.position = new Vector3(-20, 0, 0);
        yield return new WaitForSeconds(0.1f);
        brochure.SetActive(false);

    }

}
