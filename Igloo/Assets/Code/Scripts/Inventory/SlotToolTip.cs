using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField] private GameObject slotBase;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void ShowToolTip(Item target, Vector3 pos)
    {
        slotBase.SetActive(true);

        pos += new Vector3( -slotBase.GetComponent<RectTransform>().rect.width * 1.4f,
                            0,
                            0);
        slotBase.transform.position = pos;

        title.text = target.itemName;
        description.text = target.itemDesc;
    }

    public void HideToolTip()
    {
        slotBase.SetActive(false);
    }


}
