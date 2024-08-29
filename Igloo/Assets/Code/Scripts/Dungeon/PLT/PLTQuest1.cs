using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PLTQuest1 : MonoBehaviour
{
    public DragAndDropPLT[] childs;
    public TMP_Text textUI;
    public GameObject lever;

    public GameObject itemPrefab;
    //public GameObject answerFXPrefab;
    public AudioClip right;

    void Awake()
    {
        childs = GetComponentsInChildren<DragAndDropPLT>();
    }


    void Update()
    {
        
    }

    public void CheckRoots()
    {
        bool result = true;

        for (int i = 0; i < childs.Length; i++)
        {
            result = result && childs[i].getAnswer();
        }

        if (result)
        {
            textUI.text = "정답입니다!";
            RightAnswer();

            Invoke("Answer1", 2f);
        }
    }
    
    public void Answer1()
    {
        GameObject instance = Instantiate(itemPrefab);
        //Instantiate(answerFXPrefab);

        lever.GetComponent<PLTLever>().DisableLever();
    }

    public void RightAnswer()
    {
        SoundManager.instance.Play(right);
    }
}
