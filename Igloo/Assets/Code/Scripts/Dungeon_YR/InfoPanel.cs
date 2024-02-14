using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        
        text = GetComponentInChildren<Text>();

        DOTween.Init();
        transform.localScale = Vector3.one * 0.1f;
        gameObject.SetActive(false);
    }

    public void Show(string str)
    {
        gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        //seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play().OnComplete(() =>
        {
            SetText(str);
        });
    }

    public void SetText(string str)
    {
        text.DOText(str, 1f);
    }



    public void Hide()
    {
        var seq = DOTween.Sequence();

        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        //seq.Append(transform.DOScale(0.2f, 0.2f));

        seq.Play().OnComplete(() =>
        {
            text.text = null;
            gameObject.SetActive(false);
        });
    }

}
