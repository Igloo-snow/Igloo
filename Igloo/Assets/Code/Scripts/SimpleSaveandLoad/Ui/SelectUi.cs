using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectUi : MonoBehaviour
{
    public GameObject create;
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName;

    private GameObject clickedObject;

    private bool[] saveFile = new bool[3];

    public AudioSource fxAudioSource;
    public AudioSource bgmAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            SlotDataCheck(i);
        }
        DataManager.instance.DataClear();
    }

    public void Slot(int num)
    {
        DataManager.instance.nowSlot = num;
        fxAudioSource.Play();

        if (saveFile[num]) // 저장된 데이터가 있을 때
        {
            Debug.Log(num + "저장된 데이터 잇음");
            DataManager.instance.LoadData();

            StartLoadedGame();
        }
        else // 저장된 데이터가 없을 때
        {
            Debug.Log(num + "저장된 데이터 없음");

            Create();
        }
    }

    public void SlotDataDelete(int num)
    {
        DataManager.instance.DeleteLocalData(num);
        SlotDataCheck(num);
    }

    public void SlotDataCheck(int num)
    {
        if (DataManager.instance.DataExistCheck(num))
        {
            saveFile[num] = true;
            DataManager.instance.nowSlot = num;
            DataManager.instance.LoadData();
            slotText[num].text = DataManager.instance.nowPlayer.name;
            DataManager.instance.DataClear();
        }
        else
        {
            slotText[num].text = "비어있음";
            saveFile[num] = false;
        }
    }

    public void Create()
    {
        create.SetActive(true);
    }

    public void StartNewGame()
    {
        fxAudioSource.Play();
        DataManager.instance.nowPlayer.name = newPlayerName.text;
        DataManager.instance.SaveData();
        SceneManager.LoadScene(8);
    }

    public void StartLoadedGame()
    {
        SceneManager.LoadScene(1);
    }

}
