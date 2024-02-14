using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookItem : MonoBehaviour
{
    [SerializeField] private Transform[] pages;
    [SerializeField] private Transform book;

    // Start is called before the first frame update
    void Start()
    {
        CombinePages();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CombinePages()
    {
        Sequence combineSequence = DOTween.Sequence();
        foreach(Transform transform in pages)
        {
            combineSequence.Join(transform.DOMove(book.position, 1f));
        }

    }
}
