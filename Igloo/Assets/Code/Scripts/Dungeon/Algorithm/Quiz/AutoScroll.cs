using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public static AutoScroll instance;

    private ScrollRect scrollRect;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void AutoScrolling()
    {
        scrollRect.normalizedPosition = Vector3.zero;
    }
}

