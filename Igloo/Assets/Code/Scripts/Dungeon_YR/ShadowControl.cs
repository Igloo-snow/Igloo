using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowControl : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = panel.GetComponent<Image>();
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
        yield return new WaitForSeconds(2f);
        Color color = img.color;
        float a = color.a;
        a += 0.08f;
        if( a >= 0.8f)
        {
            a = 0.8f;
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
        // 변하고 잠시 기다리도록 코드 수정 필요
        Color color = img.color;
        float a = color.a;
        a = 0f;
        color.a = a;
        img.color = color;

    }
    //종료시 코루틴 멈추기 추가

}
