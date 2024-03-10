using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowControl : UiBase
{
    [SerializeField] private GameObject panel;
    private Image img;
    public float time = 2f;

    private void Start()
    {
        img = panel.GetComponent<Image>();
    }

    public void LevelTime(float f)
    {
        time = f;
        InitUI();
        StartCoroutine(ShadeCoroutine());
    }

    public void InitUI()
    {
        Color color = img.color;
        float a = 0f;
        color.a = a;
        img.color = color;

    }

    IEnumerator ShadeCoroutine()
    {
        yield return new WaitForSeconds(time);
        Color color = img.color;
        float a = color.a;

        if( a >= 0.8f )
        {
            a = 0.8f;
        }
        else
        {
            a += 0.08f;
        }
        
        color.a = a;
        img.color = color;
        StartCoroutine(ShadeCoroutine());
    }

    public void GetBrighter()
    {
        Color color = img.color;
        float a = color.a;
        if (a >= 1f)
            return;
        a -= 0.1f;
        color.a = a;
        img.color = color;
    }

    public void Restart()
    {
        Color color = img.color;
        float a = color.a;
        a = 0f;
        color.a = a;
        img.color = color;
    }

}
