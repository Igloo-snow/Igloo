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
    private int npcRemovalCount = 0;
    public float moveDuration = 1f;

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
        npcRemovalCount++;

        if (npcRemovalCount % 2 == 0 && rewardIndex < rewardItems.Length)
        {
            GameObject reward = rewardItems[rewardIndex];
            reward.SetActive(true);
            rewardIndex++;
        }

        yield return new WaitForSeconds(1f);

        MoveNPCs();

        yield return StartCoroutine(MoveNPCs());

        SpawnNPC(spawnPoints.Length - 1);
        UpdateNPCInteractivity();

        isHandlingNPCRemoval = false;
    }

    IEnumerator MoveNPCs()
    {
        for (int i = 1; i < npcs.Length; i++)
        {
            if (npcs[i] != null)
            {
                NPC currentNPC = npcs[i];
                Vector3 startPosition = currentNPC.transform.position;
                Vector3 targetPosition = spawnPoints[i - 1].position;

                float elapsedTime = 0f;
                while (elapsedTime < moveDuration)
                {
                    currentNPC.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                currentNPC.transform.position = targetPosition; // 이동이 끝나면 정확한 위치로 설정
                npcs[i - 1] = currentNPC;
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