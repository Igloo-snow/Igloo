using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data instance;

    public List<Item> items = new List<Item>();
    public Dictionary<int, int> npcData = new Dictionary<int, int>();
    //public List<Npc> npcs = new List<Npc>(); 이거 안돼 씬 전환되면서 npc 참조 잃어


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        GameEventsManager.instance.dialogueEvents.onUpdateDialogueIndex += UpdateDialogueIndex;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameEventsManager.instance.dialogueEvents.onUpdateDialogueIndex -= UpdateDialogueIndex;

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

    private void UpdateDialogueIndex(int id)
    {
        if (npcData.Count <= 0)
            return;

        if (npcData.ContainsKey(id))
        {
            Debug.Log(id + "data 스크립트 안");
            npcData[id] += 1;
        }
    }

}
