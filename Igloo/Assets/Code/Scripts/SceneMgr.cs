using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;

    private ActionController actionController;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            actionController = FindObjectOfType<ActionController>();
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

}
