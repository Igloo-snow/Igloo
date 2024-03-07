using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    public Image container;
    public Sprite sprite;
    public Transform target;

    public void Update()
    {
        ShowBubble();
    }

    public void ShowBubble()
    {
        //container.sprite = sprite;
        //container.transform.position = Camera.main.WorldToScreenPoint(target.position);
        transform.position = Camera.main.WorldToScreenPoint(target.position);

    }
}
