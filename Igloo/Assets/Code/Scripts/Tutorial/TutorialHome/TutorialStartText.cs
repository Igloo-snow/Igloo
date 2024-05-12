using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class TutorialStartText : TutorialBase
{
    [SerializeField] private Text text;
    [Tooltip("띄어쓰기는 스페이스바 2번으로")]
    [SerializeField] private string str;


    private bool isCompleted = false;


    public override void Enter()
    {
        StartCoroutine(Typing(str));
    }

    IEnumerator Typing(string str)
    {
        text.text = null;

        if (str.Contains("  ")) str = str.Replace("  ", "\n");

        for (int i = 0; i < str.Length; i++)
        {
            text.text += str[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        isCompleted = true;
    }

    private void OnAfterTextEffect()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller)
    {
        if(isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }


    public override void Exit()
    {
        text.gameObject.SetActive(false);
    }
}
