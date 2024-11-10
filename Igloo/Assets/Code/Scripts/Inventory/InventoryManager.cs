using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Dictionary<Item, int> itemCount = new Dictionary<Item, int>();

    public Transform ItemContent;
    public GameObject InventoryItemPrefab;
    public SlotToolTip slotToolTip;

    private Dictionary<Item, GameObject> itemUIObjects = new Dictionary<Item, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void InitItemUi()
    {
        foreach(Item item in Items)
        {
            CreateUIObject(item);
        }
    }

    private GameObject CreateUIObject(Item item)
    {
        GameObject obj = Instantiate(InventoryItemPrefab, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;
        obj.GetComponent<Slot>().relatedItem = item;

        itemUIObjects.Add(item, obj);

        if (itemCount[item] > 1)
        {
            var itemNum = itemUIObjects[item].transform.Find("ItemCount").GetComponent<Text>();
            itemNum.text = itemCount[item].ToString();
            itemUIObjects[item].transform.Find("ItemCount").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("ItemCount").gameObject.SetActive(false);
        }
        return obj;
    }

    public void AddFromData(ActiveItem targetActiveItem)
    {
        Debug.Log("AddFromData" + targetActiveItem.item.id);

        // 이게 없는 경우가 있네
        foreach (ActiveItem item in DataManager.instance.nowPlayer.items)
        {
            Debug.Log("AddFromData  : " + targetActiveItem.item.id);


            if (item.item == targetActiveItem.item)
            {
                Items.Add(targetActiveItem.item);
                itemCount.Add(targetActiveItem.item, targetActiveItem.count);
            }
            else
            {
                Debug.Log("AddFromData  else : " + targetActiveItem.item.id);

            }
        }

    }

    public void Add(Item item)
    {
        if (Items.Contains(item))   
        {
            itemCount[item]++;
            var itemNum = itemUIObjects[item].transform.Find("ItemCount").GetComponent<Text>();
            itemNum.text = itemCount[item].ToString();
            itemUIObjects[item].transform.Find("ItemCount").gameObject.SetActive(true);
        }
        else    
        {
            Items.Add(item);
            itemCount.Add(item, 1);
            
            if (!itemUIObjects.ContainsKey(item))
            {
                GameObject obj = CreateUIObject(item);
            }
        }
        DataAdd(item);
    }


    private void DataAdd(Item item)
    {
        if(DataManager.instance == null) { return; }

        if (itemCount[item] > 1)
        {
            for (int i = 0; i < DataManager.instance.nowPlayer.items.Count; i++)
            {
                if (DataManager.instance.nowPlayer.items[i].item == item)
                {
                    DataManager.instance.nowPlayer.items[i].count++;
                }
            }
        }
        else
        {
            ActiveItem activeItem = new ActiveItem(item, itemCount[item]);
            DataManager.instance.nowPlayer.items.Add(activeItem);
            Debug.Log("activeItem in DataAdd() : " +  activeItem.item.name);
        }
    }

    public void Remove(Item item)
    {
        // 게임 세이브를 위한 Data 업데이트
        if (DataManager.instance != null)
        {
            for (int i = 0; i < DataManager.instance.nowPlayer.items.Count; i++)
            {
                if (DataManager.instance.nowPlayer.items[i].item == item)
                {
                    if(DataManager.instance.nowPlayer.items[i].count > 1)
                    {
                        DataManager.instance.nowPlayer.items[i].count--;
                    }
                    else
                    {
                        DataManager.instance.nowPlayer.items.Remove(DataManager.instance.nowPlayer.items[i]);
                    }
                }
            }
        }

        if (itemCount[item] > 1)
        {
            itemCount[item]--;
            var itemNum = itemUIObjects[item].transform.Find("ItemCount").GetComponent<Text>();
            itemNum.text = itemCount[item].ToString();
            if (itemCount[item] == 1)
            {
                itemUIObjects[item].transform.Find("ItemCount").gameObject.SetActive(false);
            }
        }
        else
        {
            if (itemUIObjects.ContainsKey(item))
            {
                Destroy(itemUIObjects[item]);
                itemUIObjects.Remove(item);
            }
            Items.Remove(item);
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

    public void ShowTooltip(Item item, Vector3 pos)
    {
        slotToolTip.ShowToolTip(item, pos);
    }

    public void HideTooltip()
    {
        slotToolTip.HideToolTip();
    }
}