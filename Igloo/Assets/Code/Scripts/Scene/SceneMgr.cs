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
                SoundManager.instance.Play("071_Unequip_01", Sound.Effect, 1, 0.2f);
                //Debug.Log("Pro :" + asyncOperation.progress);

                //���� �ʱ�ȭ
                asyncOperation.allowSceneActivation = true;

            }
            yield return null;
        }
    }
}
