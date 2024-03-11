using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class PlayerData
{
    // �̸�, ���� ������, ����Ʈ �����Ȳ
    public string name;
    public List<Item> items = new List<Item>();
    //public Dictionary<string, Quest> quests;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region �̱���
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
}
