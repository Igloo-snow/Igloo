using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubbleBase : MonoBehaviour
{
    public TMP_Text text;
    protected float hMargin = 1;
    protected float vMargin = 1;

    [SerializeField] private RectTransform textRect;
    [SerializeField] private RectTransform thisRect;

    protected void ResizeText()
    {
        textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, text.GetComponent<TMP_Text>().preferredHeight);
        thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x * hMargin, text.GetComponent<TMP_Text>().preferredHeight * vMargin);
    }

    protected void ResizeText(float width, float height)
    {
        textRect.sizeDelta = new Vector2(width, height);
        thisRect.sizeDelta = new Vector2(width, height);
    }


}
