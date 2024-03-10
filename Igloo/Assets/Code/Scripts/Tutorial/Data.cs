using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data instance;

    public List<Item> items = new List<Item>();
    //public Dictionary<Npc, int> npcData = new Dictionary<Npc, int>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InventoryManager inven = FindObjectOfType<InventoryManager>();
        if(inven != null)
        {
            foreach (var item in items)
            {
                inven.Items.Add(item);
            }
            inven.InitItemUi();
        }


    }


}
