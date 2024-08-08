using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private GameObject itemPrefabInstance;
    private string desiredItemTag;
    private Collider colliderComponent;

    private void Awake()
    {
        colliderComponent = GetComponent<Collider>();
    }

    public void Setup(string itemTag, GameObject itemPrefab)
    {
        desiredItemTag = itemTag;
        ShowDesiredItemPrefab(itemPrefab);
    }

    void ShowDesiredItemPrefab(GameObject itemPrefab)
    {
        if (itemPrefab == null) return;

        if (itemPrefabInstance != null)
        {
            Destroy(itemPrefabInstance);
        }

        itemPrefabInstance = Instantiate(itemPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        itemPrefabInstance.transform.SetParent(transform);
        itemPrefabInstance.transform.localPosition = Vector3.up * 2;
    }

    public bool IsItemCorrect(GameObject item)
    {
        return item.CompareTag(desiredItemTag);
    }

    public void ReceiveItem(GameObject item)
    {
        Destroy(gameObject);
        if (itemPrefabInstance != null)
        {
            Destroy(itemPrefabInstance);
        }
    }

    public void SetColliderActive(bool active)
    {
        if (colliderComponent != null)
        {
            colliderComponent.enabled = active;
        }
    }
}