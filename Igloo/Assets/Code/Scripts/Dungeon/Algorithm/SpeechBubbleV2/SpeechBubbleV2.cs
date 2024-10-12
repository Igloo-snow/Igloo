using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeechBubbleV2 : SpeechBubbleBase
{
    public string content;
    public Transform target;
    private Vector2 originalSize = new Vector2(500, 100);

    public void TextOn()
    {
        hMargin = 1.2f;
        vMargin = 3;
        text.text = content;
        ResizeText();
        SetPosition();
    }

    public void TextOff()
    {
        ResizeText(originalSize.x, originalSize.y);
    }

    private void SetPosition()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.position);

        // ȭ�� ������ ������ �ʵ��� ��ǥ�� ����
        float thisW = GetComponent<RectTransform>().sizeDelta.x;
        float thisH = GetComponent<RectTransform>().sizeDelta.y;
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0 + thisW, Screen.width - thisW);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height - thisH);


        // UI �̹����� ��ġ�� ����
        transform.position = screenPosition;
    }

}
