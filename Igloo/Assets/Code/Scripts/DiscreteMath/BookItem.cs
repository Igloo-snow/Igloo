using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookItem : Interactable
{
    [SerializeField] private Transform[] pages;
    [SerializeField] private Transform book;

    public bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        //CombinePages();
    }

    public void CombinePages()
    {
        Sequence combineSequence = DOTween.Sequence();
        foreach(Transform transform in pages)
        {
            combineSequence.Join(transform.DOMove(book.position, 1f));
        }
        combineSequence.AppendCallback(AppearBook);
    }

    private void AppearBook()
    {
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
        book.gameObject.SetActive(true);
    }

}
