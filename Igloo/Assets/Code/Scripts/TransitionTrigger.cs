using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : Interactable
{

    [SerializeField] private SceneNames nextScene;

    private SceneMgr sceneMgr;
    private SceneName sceneName;


    private void Start()
    {
        sceneName = GetComponent<SceneName>();
        sceneMgr = FindObjectOfType<SceneMgr>();

        string n = sceneName.SwitchName(nextScene);
        promptMessage = n + "���� �̵� (EŰ)";
    }

    protected override void Interact()
    {
        sceneMgr.StartLoadScene((int)nextScene);
    }

}
