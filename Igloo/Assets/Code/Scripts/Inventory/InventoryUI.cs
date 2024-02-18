using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private GameObject inventoryUI; // 인벤토리 UI 오브젝트

    void Start()
    {
        // 부모 오브젝트의 자식 오브젝트 중에서 "InventoryUI"를 찾음
        inventoryUI = transform.Find("InventoryUI").gameObject;
        inventoryUI.SetActive(false); // 시작 시 비활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 인벤토리 UI의 활성화/비활성화 토글
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}