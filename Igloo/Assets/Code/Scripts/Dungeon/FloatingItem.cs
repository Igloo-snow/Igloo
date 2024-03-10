using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class FloatingItem : MonoBehaviour
{
    public BasePanel panel;

    [SerializeField] private float height = 2f;
    [SerializeField] private string str;

    [SerializeField] private Transform itemTransform;
    [SerializeField] private GameObject particle;

    private float itmeTweeningTime = 0.5f;
    [SerializeField] private float TextweeningTime = 0.5f;

    private int count = 1;

    private void Start()
    {
        panel = FindObjectOfType<BasePanel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (count == 1)
            {
                ItemAppear();
                count--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle.SetActive(false);
        }
    }

    private void ItemAppear()
    {
        Sequence itemSequence = DOTween.Sequence();
        itemSequence.Append(itemTransform.DOMoveY(itemTransform.position.y + height, itmeTweeningTime).SetUpdate(true));
        itemSequence.AppendCallback(UiAppear).SetUpdate(true);

        Vector3 rot = new Vector3(-30, itemTransform.eulerAngles.y, itemTransform.eulerAngles.z);
        itemSequence.Append(itemTransform.DORotate(rot, itmeTweeningTime)).SetUpdate(true);
    }

    private void UiAppear()
    {
        panel.InfoOn(str, TextweeningTime);
    }    

}
