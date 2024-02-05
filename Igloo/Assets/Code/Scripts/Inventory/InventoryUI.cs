using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        gameObject.SetActive(isInventoryOpen);
    }
}