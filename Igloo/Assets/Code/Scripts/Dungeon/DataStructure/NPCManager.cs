using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs; // NPC ������ �迭
    public Transform[] spawnPoints; // NPC ���� ����Ʈ �迭
    public GameObject[] itemPrefabs; // ������ ������ �迭
    private NPC[] npcs; // ���� ������ NPC���� �����ϴ� �迭
    private bool isHandlingNPCRemoval = false; // NPC ���� ó���� ���� ������ ����

    void Start()
    {
        npcs = new NPC[spawnPoints.Length]; // NPC �迭 �ʱ�ȭ
        SpawnInitialNPCs(); // �ʱ� NPC ����
    }

    // ������ �����ǿ� NPC�� �����ϴ� �޼���
    void SpawnNPC(int index)
    {
        if (index >= 0 && index < spawnPoints.Length)
        {
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)]; // �������� NPC ������ ����
            GameObject npcObject = Instantiate(npcPrefab, spawnPoints[index].position, Quaternion.identity); // NPC �ν��Ͻ� ����
            NPC npc = npcObject.GetComponent<NPC>(); // NPC ������Ʈ ��������
            if (npc != null)
            {
                npcs[index] = npc; // NPC �迭�� �߰�
                GameObject itemPrefab = GetRandomItemPrefab(); // ���� ������ ������ ����
                npc.Setup(itemPrefab.tag, itemPrefab); // NPC�� ������ ����
            }
        }
    }

    // �ʱ� NPC�� �� ���� ����Ʈ�� �����ϴ� �޼���
    void SpawnInitialNPCs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnNPC(i); // �� �����ǿ� NPC ����
        }
        UpdateNPCInteractivity(); // NPC�� ��ȣ�ۿ� ���� ������Ʈ
    }

    void Update()
    {
        if (!isHandlingNPCRemoval) // NPC ���� ó���� ���� ������ �ʴٸ�
        {
            CheckAndHandleNPCs(); // �� �����Ӹ��� NPC ���� üũ
        }
    }

    // NPC�� ��������� Ȯ���ϰ� �ʿ��� �۾��� �����ϴ� �޼���
    void CheckAndHandleNPCs()
    {
        if (npcs[0] == null) // 1�� �������� NPC�� ������ٸ�
        {
            StartCoroutine(HandleNPCRemoval()); // NPC ���� �� ���ġ �ڷ�ƾ ����
        }
    }

    // NPC�� ������� �� ȣ��Ǿ� NPC�� �̵���Ű�� ���ο� NPC�� �����ϴ� �ڷ�ƾ
    IEnumerator HandleNPCRemoval()
    {
        isHandlingNPCRemoval = true; // NPC ���� ó�� ����

        yield return new WaitForSeconds(1f); // 1�� ���

        MoveNPCs(); // NPC �̵� ó��

        yield return new WaitForSeconds(1f); // NPC �̵� �� 1�� ���

        // �� 5�� �����ǿ� ���ο� NPC�� ����
        SpawnNPC(spawnPoints.Length - 1);
        UpdateNPCInteractivity(); // NPC�� ��ȣ�ۿ� ���� ������Ʈ

        isHandlingNPCRemoval = false; // NPC ���� ó�� �Ϸ�
    }

    // NPC���� �̵���Ű�� �޼���
    void MoveNPCs()
    {
        for (int i = 1; i < npcs.Length; i++)
        {
            if (npcs[i] != null) // ���� �����ǿ� NPC�� �ִٸ�
            {
                npcs[i - 1] = npcs[i]; // NPC�� �� ĭ ������ �̵�
                npcs[i - 1].transform.position = spawnPoints[i - 1].position; // ���ο� ���������� �̵�
                npcs[i] = null; // ���� ��ġ�� NPC ������ null�� ����
            }
        }
    }

    // NPC�� ��ȣ�ۿ� ���¸� ������Ʈ�ϴ� �޼���
    void UpdateNPCInteractivity()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i] != null) // NPC�� �����ϴ� ���
            {
                npcs[i].SetColliderActive(i == 0); // 1�� �������� NPC�� �ݶ��̴� Ȱ��ȭ
            }
        }
    }

    // �������� ������ �������� �����ϴ� �޼���
    GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)]; // ������ ������ �迭���� ���� ����
    }

    // 1�� �������� NPC�� ��ȯ�ϴ� �޼���
    public NPC GetFrontNPC()
    {
        return npcs[0]; // ���� 1�� �������� NPC�� ��ȯ
    }
}