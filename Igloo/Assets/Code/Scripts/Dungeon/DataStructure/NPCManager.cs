using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs;
    public Transform[] spawnPoints;
    public GameObject[] itemPrefabs;
    public GameObject[] rewardItems;

    private NPC[] npcs;
    private bool isHandlingNPCRemoval = false;
    private int rewardIndex = 0;

    void Start()
    {
        npcs = new NPC[spawnPoints.Length];
        SpawnInitialNPCs();

        foreach (GameObject reward in rewardItems)
        {
            reward.SetActive(false);
        }
    }

    void SpawnNPC(int index)
    {
        if (index >= 0 && index < spawnPoints.Length)
        {
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
            GameObject npcObject = Instantiate(npcPrefab, spawnPoints[index].position, Quaternion.identity);
            NPC npc = npcObject.GetComponent<NPC>();
            if (npc != null)
            {
                npcs[index] = npc;
                GameObject itemPrefab = GetRandomItemPrefab();
                npc.Setup(itemPrefab.tag, itemPrefab);
            }
        }
    }

    void SpawnInitialNPCs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnNPC(i);
        }
        UpdateNPCInteractivity();
    }

    void Update()
    {
        if (!isHandlingNPCRemoval)
        {
            CheckAndHandleNPCs();
        }
    }

    void CheckAndHandleNPCs()
    {
        if (npcs[0] == null)
        {
            StartCoroutine(HandleNPCRemoval());
        }
    }

    IEnumerator HandleNPCRemoval()
    {
        isHandlingNPCRemoval = true;

        if (rewardIndex < rewardItems.Length)
        {
            GameObject reward = rewardItems[rewardIndex];
            reward.SetActive(true);
            rewardIndex++;
        }

        yield return new WaitForSeconds(1f);

        MoveNPCs();

        yield return new WaitForSeconds(1f);

        SpawnNPC(spawnPoints.Length - 1);
        UpdateNPCInteractivity();

        isHandlingNPCRemoval = false;
    }

    void MoveNPCs()
    {
        for (int i = 1; i < npcs.Length; i++)
        {
            if (npcs[i] != null)
            {
                npcs[i - 1] = npcs[i];
                npcs[i - 1].transform.position = spawnPoints[i - 1].position;
                npcs[i] = null;
            }
        }
    }

    void UpdateNPCInteractivity()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i] != null)
            {
                npcs[i].SetColliderActive(i == 0);
            }
        }
    }

    GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }

    public NPC GetFrontNPC()
    {
        return npcs[0];
    }
}