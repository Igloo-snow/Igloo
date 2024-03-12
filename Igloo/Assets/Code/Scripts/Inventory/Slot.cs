using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item relatedItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(relatedItem != null)
        {
            InventoryManager.Instance.ShowTooltip(relatedItem, this.transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.HideTooltip();
    }
}
