using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private GameObject uIImg;
    private Text text;
    private Button btn;
    [SerializeField] private float height = 2f;
    [SerializeField] private string str;
    [SerializeField] private Transform itemMesh;
    [SerializeField] private GameObject particle;
    [SerializeField] private float tweeningTime = 0.5f;

    private int count = 1;

    private void Start()
    {
        text = uIImg.gameObject.GetComponentInChildren<Text>();
        btn = uIImg.gameObject.GetComponentInChildren<Button>();
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
            text.gameObject.SetActive(false);
            particle.SetActive(false);
        }
    }

    private void ItemAppear()
    {
        
        Sequence itemSequence = DOTween.Sequence();
        itemSequence.Append(itemMesh.DOMoveY(itemMesh.position.y + height, tweeningTime).SetUpdate(true));
        Vector3 rot = new Vector3(-30, itemMesh.eulerAngles.y, itemMesh.eulerAngles.z);
        itemSequence.Append(itemMesh.DORotate(rot, tweeningTime).SetUpdate(true));
        itemSequence.AppendCallback(ImgAppear).SetUpdate(true);

        //itemSequence.Play();
        GameManager.isOpenInfoUI = true;
    }

    private void TextAppear()
    {
        text.gameObject.SetActive(true);
        text.text = null;
        text.DOText(str, 1f).SetUpdate(true);
    }

    private void ImgAppear()
    {
        uIImg.gameObject.SetActive(true);
        btn.gameObject.SetActive(true);

        TextAppear();
    }



}
