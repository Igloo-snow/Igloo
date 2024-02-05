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
        StartCoroutine(ShadeCoroutine());
    }

    IEnumerator ShadeCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Color color = img.color;
        float a = color.a;
        a += 0.1f;
        if( a >= 0.9f)
        {
            a = 0.9f;
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
        // ���ϰ� ��� ��ٸ����� �ڵ� ���� �ʿ�
        Color color = img.color;
        float a = color.a;
        a = 0f;
        color.a = a;
        img.color = color;

    }
    //����� �ڷ�ƾ ���߱� �߰�

}
