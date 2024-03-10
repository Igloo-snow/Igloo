using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    //public static TutorialController instance;
    [SerializeField] private List<TutorialBase> tutorials;


    private TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

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
            return;
        }

        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();
    }

    private void CompletedAllTutorials()
    {
        currentTutorial = null;

        // 튜토리얼 종료 시 수행할 행동
        Debug.Log("Complete All");
        
    }


}
