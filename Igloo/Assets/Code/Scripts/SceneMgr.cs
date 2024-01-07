using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance = null;

    private ActionController actionController;

    private void Start()
    {
        actionController = FindObjectOfType<ActionController>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(actionController.changingScene && actionController.nextSceneName != null)
        {
            TransferScene();
        }
    }

    private void TransferScene()
    {
        SceneManager.LoadScene(actionController.nextSceneName);
    }
}
