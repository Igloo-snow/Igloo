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
    public AudioClip wrong;

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
            if (childs[i].isHit == false)
            {
                return;
            }
        }

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
        else
            WrongAnswer();
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

    public void WrongAnswer()
    {
        SoundManager.instance.Play(wrong);
    }
}
