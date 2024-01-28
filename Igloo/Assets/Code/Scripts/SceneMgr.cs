using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;

    private ActionController actionController;
    private StartPoint startPoint;

    private string sceneName;
    public string previousScene;


    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            actionController = FindObjectOfType<ActionController>();
            startPoint = FindObjectOfType<StartPoint>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(actionController.changingScene && actionController.nextScene != "")
        {
            previousScene = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        sceneName = actionController.nextScene;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        
        
        while(!asyncOperation.isDone)
        {
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%"; 
            if (asyncOperation.progress >= 0.9f)
            {
                Debug.Log("Pro :" + asyncOperation.progress);
                //변수 초기화
                actionController.encountered = false;
                asyncOperation.allowSceneActivation = true;

            }
            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 새로운 씬이 로드될 때 초기화 코드 실행
        actionController = FindObjectOfType<ActionController>();
        Debug.Log("씬 세팅");
    }
}
