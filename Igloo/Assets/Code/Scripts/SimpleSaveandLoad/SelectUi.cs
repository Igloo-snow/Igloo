using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUi : MonoBehaviour
{
    public GameObject create;
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName;

    private bool[] saveFile = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (DataManager.instance.DataExistCheck(i))
            {
                saveFile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;
                DataManager.instance.DataClear();
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int num)
    {
        DataManager.instance.nowSlot = num;

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

    public void Create()
    {
        create.SetActive(true);
    }

    public void StartNewGame()
    {
        DataManager.instance.nowPlayer.name = newPlayerName.text;
        DataManager.instance.SaveData();
        SceneManager.LoadScene(8);
    }

    public void StartLoadedGame()
    {
        SceneManager.LoadScene(1);
    }
}
