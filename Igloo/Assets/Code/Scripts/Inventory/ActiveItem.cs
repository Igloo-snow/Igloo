using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveItem
{
    public Item item;
    public int count;

    public ActiveItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
