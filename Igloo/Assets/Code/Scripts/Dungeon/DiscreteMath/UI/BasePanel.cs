using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image[] lifeImg;

    [Header("InfoUi")]
    public Image backgroundImg;
    public Text text;
    public Button InfoOffButton;

    private void Start()
    {
        if(InfoOffButton != null )
        {
            InfoOffButton.onClick.AddListener(InfoOff);
        }
    }

    public void InfoOn(string str, float time)
    {
        GameManager.isStaticUiOpen = true;
        text.text = null;
        backgroundImg.gameObject.SetActive(true);
        InfoOffButton.gameObject.SetActive(true);
        
        text.gameObject.SetActive(true);
        
        text.DOText(str, time).SetUpdate(true);
    }

    public void InfoOff()
    {
        backgroundImg.gameObject.SetActive(false);
        GameManager.isStaticUiOpen = false;
    }

    public void UpdateLifeUI(int current)
    {
        for(int i = lifeImg.Length - 1; i >= current; i--)
        {
            lifeImg[i].color = new Color(1, 1, 1, 0.1f);
        }
    }

    public void LifeUIOff()
    {
        for(int i = 0; i < lifeImg.Length; i++)
        {
            lifeImg[i].color = new Color(1, 1, 1, 0.1f);
        }
    }

    public void LifeUIOn()
    {
        for (int i = 0; i < lifeImg.Length; i++)
        {
            lifeImg[i].color = new Color(1, 1, 1, 1f);
        }
    }
}
