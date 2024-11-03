using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBar;
    public Image backgroundBar;

    [Header("Character")]
    [SerializeField] GameObject character;
    [SerializeField] int maxHealth = 0;
    private int currentHealth = 0;
    //public float damage = 100;
    private float maxWidth;
    float imgWidth;
    private int deathCall = 1;

    private void OnEnable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit += InitHealth;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit -= InitHealth;
    }

    private void Start()
    {

        InitHealth();
    }

    private void InitHealth()
    {
        if(deathCall == 1)
        {
            maxWidth = healthBar.rectTransform.sizeDelta.x;
        }
        else
        {
            deathCall = 1;
        }

        currentHealth = maxHealth;
        if (character.GetComponent<ICharacterState>() != null)
        {
            maxHealth = character.GetComponent<ICharacterState>().GetHealth();
            currentHealth = maxHealth;

            imgWidth = maxWidth;
            healthBar.rectTransform.sizeDelta = new Vector2(imgWidth, healthBar.rectTransform.sizeDelta.y);
            backgroundBar.rectTransform.sizeDelta = new Vector2(imgWidth, healthBar.rectTransform.sizeDelta.y);
        }
        else
        {
            Debug.Log("get 실패");
            currentHealth = 10000;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && deathCall > 0)
        {
            currentHealth = 0;
            character.GetComponent<ICharacterState>().Die();
            deathCall--;
        }
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

    public void decreaseHealth(int damage = 100)
    {
        currentHealth -= damage;
        ResizeHealthBar();

    }


}
