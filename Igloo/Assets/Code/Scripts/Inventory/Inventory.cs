using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image itemImage;

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}