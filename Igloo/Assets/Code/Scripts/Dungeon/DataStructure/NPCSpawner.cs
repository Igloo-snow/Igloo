using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public List<GameObject> itemPrefabs;
    public int npcCount = 5;
    public float spacing = 1.0f;
    private List<GameObject> npcs;

    public static NPCSpawner Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        npcs = new List<GameObject>();
        for (int i = 0; i < npcCount; i++)
        {
            SpawnNPC(i);
        }
    }

    void SpawnNPC(float zPosition)
    {
        Vector3 position = new Vector3(10, 2, zPosition);
        GameObject newNpc = Instantiate(npcPrefab, position, Quaternion.identity);
        NPC npcScript = newNpc.GetComponent<NPC>();
        GameObject randomItem = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Count)], new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity); // 랜덤 위치에 아이템 생성
        npcScript.desiredItem = randomItem;
        npcs.Add(newNpc);

        Debug.Log($"Spawned NPC at Position: {position}, assigned desiredItem: {randomItem.name}");
    }

    public List<GameObject> GetNPCs()
    {
        return npcs;
    }

    public IEnumerator ShiftNPCsForwardWithDelay(float forwardDelay, float spawnDelay)
    {
        yield return new WaitForSeconds(forwardDelay);

        for (int i = 0; i < npcs.Count; i++)
        {
            if (npcs[i] != null)
            {
                npcs[i].transform.position -= new Vector3(0, 0, spacing);
                Debug.Log($"Shifted NPC {i} to Position: {npcs[i].transform.position.z}");
            }
        }
        npcs.RemoveAt(0);

        yield return new WaitForSeconds(spawnDelay);

        SpawnNPC(npcCount - 1);
    }
}