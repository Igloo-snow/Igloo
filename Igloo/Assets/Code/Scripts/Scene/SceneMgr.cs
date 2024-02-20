using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public void StartLoadScene(int sceneNum)
    {
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        StartCoroutine(LoadScene(sceneNum));
    }

    IEnumerator LoadScene(int sceneNum)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNum);
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        
        
        while(!asyncOperation.isDone)
        {
            //m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%"; 
            if (asyncOperation.progress >= 0.9f)
            {
                //Debug.Log("Pro :" + asyncOperation.progress);

                //변수 초기화
                asyncOperation.allowSceneActivation = true;

            }
            yield return null;
        }
    }
}
