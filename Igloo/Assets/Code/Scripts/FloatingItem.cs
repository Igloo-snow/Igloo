using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float height = 2f;
    [SerializeField] private string str;
    [SerializeField] private Transform itemMesh;
    [SerializeField] private GameObject particle;

    private int count = 1;

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
        itemSequence.Append(itemMesh.DOMoveY(itemMesh.position.y + height, 1.5f));
        Vector3 rot = new Vector3(-30, itemMesh.eulerAngles.y, itemMesh.eulerAngles.z);
        itemSequence.Append(itemMesh.DORotate(rot, 0.8f));
        itemSequence.AppendCallback(TextAppear);

        //itemSequence.Play();
    }

    private void TextAppear()
    {
        text.gameObject.SetActive(true);
        text.text = null;
        text.DOText(str, 1f);
    }



}
