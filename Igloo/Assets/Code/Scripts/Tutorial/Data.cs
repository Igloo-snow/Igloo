using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class npcData
{
    public int npcId;
    public int index;
}

public class Data : MonoBehaviour
{
    public static Data instance;

    //public List<npcData> npcs; 
    //public Dictionary<int, int> npcData = new Dictionary<int, int>();
    //public List<Npc> npcs = new List<Npc>(); ÀÌ°Å ¾ÈµÅ ¾À ÀüÈ¯µÇ¸é¼­ npc ÂüÁ¶ ÀÒ¾î


    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onUpdateDialogueIndex += UpdateDialogueIndex;
    }

    private void OnDisable()
    {
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

    private void UpdateDialogueIndex(int id)
    {
        if (DataManager.instance.nowPlayer.npcs.Count <= 0)
            return;

        foreach(npcData npc in DataManager.instance.nowPlayer.npcs)
        {
            if(npc.npcId == id)
            {
                npc.index++;
                Debug.Log(npc.npcId + npc.index);
            }
        }
    }

}
