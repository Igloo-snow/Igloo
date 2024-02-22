using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialBase> tutorials;

    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    private void Start()
    {
        SetNextTutorial();
    }

    private void Update()
    {
        if(currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        if ( currentTutorial != null )
        {
            currentTutorial.Exit();
        }

        if( currentIndex >= tutorials.Count-1)
        {
            Debug.Log("in settexttutorial");
            CompletedAllTutorials();
        }

        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();
    }

    private void CompletedAllTutorials()
    {
        currentTutorial = null;

        // Ʃ�丮�� ���� �� ������ �ൿ
        Debug.Log("Complete All");
        
    }


}
