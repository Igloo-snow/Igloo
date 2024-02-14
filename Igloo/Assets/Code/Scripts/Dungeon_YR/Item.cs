using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Transform itemMesh;
    [SerializeField] private float height;
    [SerializeField] private GameObject panel;
    [SerializeField] private string str;
    [SerializeField] private GameObject particle;
    //효과 동작 횟수
    private int activeCount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(activeCount == 1) {
                ItemAppear();
                activeCount--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false);
            particle.SetActive(false);

        }
    }

    private void ItemAppear()
    {
        Sequence itemSequence = DOTween.Sequence();
        itemSequence.Append(itemMesh.DOMoveY(itemMesh.position.y + height, 1.5f));
        itemSequence.Append(itemMesh.DORotate(new Vector3(-30, 0, 0), 0.8f));
        itemSequence.AppendCallback(TextAppear);

        itemSequence.Play();
    }

    private void TextAppear()
    {
        panel.SetActive(true);
        panel.GetComponentInChildren<Text>().text = null;
        panel.GetComponentInChildren<Text>().DOText(str, 1f);
    }
}
