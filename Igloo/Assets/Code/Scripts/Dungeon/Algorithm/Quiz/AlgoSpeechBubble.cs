using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlgoSpeechBubble : MonoBehaviour
{
    public TMP_Text speakerName;
    public TMP_Text text;

    [SerializeField] private RectTransform textRect;
    [SerializeField] private RectTransform thisRect;

    private float height;

    private void Awake()
    {
        ResizeText();
    }

    private void ResizeText()
    {
        textRect.sizeDelta = new Vector2(textRect.sizeDelta.x, text.GetComponent<TMP_Text>().preferredHeight);
        thisRect.sizeDelta = new Vector2(thisRect.sizeDelta.x, text.GetComponent<TMP_Text>().preferredHeight);
    }
}
