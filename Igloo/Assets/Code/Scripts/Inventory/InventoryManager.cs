using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItemPrefab;

    private Dictionary<Item, GameObject> itemUIObjects = new Dictionary<Item, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void InitItemUi()
    {
        foreach(var item in DataManager.instance.nowPlayer.items)
        {
            GameObject obj = Instantiate(InventoryItemPrefab, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemUIObjects.Add(item, obj);
        }
    }

    public void Add(Item item)
    {
        if(DataManager.instance != null)
        {
            //Data.instance.items.Add(item);
            DataManager.instance.nowPlayer.items.Add(item);
        }
        Items.Add(item);
        if (!itemUIObjects.ContainsKey(item))
        {
            GameObject obj = Instantiate(InventoryItemPrefab, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            itemUIObjects.Add(item, obj);
        }
    }

    public void Remove(Item item)
    {
        if (DataManager.instance != null)
        {
            //Data.instance.items.Remove(item);
            DataManager.instance.nowPlayer.items.Remove(item);
        }
        Items.Remove(item);
        if (itemUIObjects.ContainsKey(item))
        {
            Destroy(itemUIObjects[item]);
            itemUIObjects.Remove(item);
        }
    }

    public void ListItems()
    {
        foreach (var item in Items)
        {
            if (itemUIObjects.ContainsKey(item))
            {
                GameObject obj = itemUIObjects[item];
                obj.SetActive(true);
            }
        }
    }

    public bool CheckInventory(int id)
    {
        foreach(Item item in Items)
        {
            if (item.id == id)
                return true;
        }
        return false;
    }
}