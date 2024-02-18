using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Canvas inventoryCanvas;
    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();
        inventoryCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryCanvas.enabled = isInventoryOpen;
        }
    }
}