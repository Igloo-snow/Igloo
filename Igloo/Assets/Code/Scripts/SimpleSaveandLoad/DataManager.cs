using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class PlayerData
{
    // 이름, 보유 아이템, 퀘스트 진행상황
    public string name;
    public List<Item> items = new List<Item>(); 
    //public Dictionary<string, Quest> quests;
    public List<npcData> npcs = new List<npcData>();
    public List<Quest> quests = new List<Quest>();
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        path = Application.persistentDataPath + "/save";
        
        foreach (npcData npc in DataManager.instance.nowPlayer.npcs)
        {
            Debug.Log(npc.npcId + npc.index);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("dataManager onsceneloaded");
        InventoryManager inven = FindObjectOfType<InventoryManager>();
        if (inven != null && nowPlayer.items.Count > 0)
        {
            foreach (var item in nowPlayer.items)
            {
                inven.Items.Add(item);
            }
            inven.InitItemUi();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log(data);
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    public bool DataExistCheck(int slotNum)
    {
        if (File.Exists(path + $"{slotNum}"))
        {
            return true;
        }
        else
            return false;
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    public void DeleteLocalData(int n)
    {
        string target = path + n;
        File.Delete(target);
    }
}
