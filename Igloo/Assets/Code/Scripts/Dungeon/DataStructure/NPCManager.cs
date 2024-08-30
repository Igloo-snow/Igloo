using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameObject[] npcPrefabs; // NPC 프리팹 배열
    public Transform[] spawnPoints; // NPC 스폰 포인트 배열
    public GameObject[] itemPrefabs; // 아이템 프리팹 배열
    private NPC[] npcs; // 현재 스폰된 NPC들을 저장하는 배열
    private bool isHandlingNPCRemoval = false; // NPC 제거 처리가 진행 중인지 여부

    void Start()
    {
        npcs = new NPC[spawnPoints.Length]; // NPC 배열 초기화
        SpawnInitialNPCs(); // 초기 NPC 스폰
    }

    // 지정된 포지션에 NPC를 스폰하는 메서드
    void SpawnNPC(int index)
    {
        if (index >= 0 && index < spawnPoints.Length)
        {
            GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)]; // 랜덤으로 NPC 프리팹 선택
            GameObject npcObject = Instantiate(npcPrefab, spawnPoints[index].position, Quaternion.identity); // NPC 인스턴스 생성
            NPC npc = npcObject.GetComponent<NPC>(); // NPC 컴포넌트 가져오기
            if (npc != null)
            {
                npcs[index] = npc; // NPC 배열에 추가
                GameObject itemPrefab = GetRandomItemPrefab(); // 랜덤 아이템 프리팹 선택
                npc.Setup(itemPrefab.tag, itemPrefab); // NPC에 아이템 설정
            }
        }
    }

    // 초기 NPC를 각 스폰 포인트에 스폰하는 메서드
    void SpawnInitialNPCs()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnNPC(i); // 각 포지션에 NPC 스폰
        }
        UpdateNPCInteractivity(); // NPC의 상호작용 상태 업데이트
    }

    void Update()
    {
        if (!isHandlingNPCRemoval) // NPC 제거 처리가 진행 중이지 않다면
        {
            CheckAndHandleNPCs(); // 매 프레임마다 NPC 상태 체크
        }
    }

    // NPC가 사라졌는지 확인하고 필요한 작업을 수행하는 메서드
    void CheckAndHandleNPCs()
    {
        if (npcs[0] == null) // 1번 포지션의 NPC가 사라졌다면
        {
            StartCoroutine(HandleNPCRemoval()); // NPC 제거 및 재배치 코루틴 시작
        }
    }

    // NPC가 사라졌을 때 호출되어 NPC를 이동시키고 새로운 NPC를 스폰하는 코루틴
    IEnumerator HandleNPCRemoval()
    {
        isHandlingNPCRemoval = true; // NPC 제거 처리 시작

        yield return new WaitForSeconds(1f); // 1초 대기

        MoveNPCs(); // NPC 이동 처리

        yield return new WaitForSeconds(1f); // NPC 이동 후 1초 대기

        // 빈 5번 포지션에 새로운 NPC를 스폰
        SpawnNPC(spawnPoints.Length - 1);
        UpdateNPCInteractivity(); // NPC의 상호작용 상태 업데이트

        isHandlingNPCRemoval = false; // NPC 제거 처리 완료
    }

    // NPC들을 이동시키는 메서드
    void MoveNPCs()
    {
        for (int i = 1; i < npcs.Length; i++)
        {
            if (npcs[i] != null) // 현재 포지션에 NPC가 있다면
            {
                npcs[i - 1] = npcs[i]; // NPC를 한 칸 앞으로 이동
                npcs[i - 1].transform.position = spawnPoints[i - 1].position; // 새로운 포지션으로 이동
                npcs[i] = null; // 원래 위치의 NPC 참조를 null로 설정
            }
        }
    }

    // NPC의 상호작용 상태를 업데이트하는 메서드
    void UpdateNPCInteractivity()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i] != null) // NPC가 존재하는 경우
            {
                npcs[i].SetColliderActive(i == 0); // 1번 포지션의 NPC만 콜라이더 활성화
            }
        }
    }

    // 랜덤으로 아이템 프리팹을 선택하는 메서드
    GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)]; // 아이템 프리팹 배열에서 랜덤 선택
    }

    // 1번 포지션의 NPC를 반환하는 메서드
    public NPC GetFrontNPC()
    {
        return npcs[0]; // 현재 1번 포지션의 NPC를 반환
    }
}