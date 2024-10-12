using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;
    public Image backgroundBar;

    public float maxHealth = 1000;
    public float currentHealth;
    //public float damage = 100;
    private float maxWidth;
    float imgWidth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        maxWidth = healthBar.rectTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ResizeHealthBar()
    {
        imgWidth = (currentHealth * maxWidth) / maxHealth;
        healthBar.rectTransform.sizeDelta = new Vector2(imgWidth, healthBar.rectTransform.sizeDelta.y);
        StartCoroutine(ResizeCoroutine());
    }

    IEnumerator ResizeCoroutine()
    {
        float diff = 10f; //damage 비율에 따른 값 변화 필요
        do
        {
            backgroundBar.rectTransform.sizeDelta = 
                new Vector2(backgroundBar.rectTransform.sizeDelta.x-diff, healthBar.rectTransform.sizeDelta.y);
            yield return new WaitForSeconds(0.04f);
        } while (backgroundBar.rectTransform.sizeDelta.x > imgWidth);
        backgroundBar.rectTransform.sizeDelta =
                new Vector2(imgWidth, healthBar.rectTransform.sizeDelta.y);
    }

    public void decreaseHealth(float damage = 100)
    {
        currentHealth -= damage;
        ResizeHealthBar();

    }
}
