using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private GameObject inventoryUI; // �κ��丮 UI ������Ʈ

    void Start()
    {
        // �θ� ������Ʈ�� �ڽ� ������Ʈ �߿��� "InventoryUI"�� ã��
        inventoryUI = transform.Find("InventoryUI").gameObject;
        inventoryUI.SetActive(false); // ���� �� ��Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // �κ��丮 UI�� Ȱ��ȭ/��Ȱ��ȭ ���
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}