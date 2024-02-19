using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [SerializeField] private Image[] lifeImg;
    [SerializeField] private Button InfoOffButton;

    private void Start()
    {
        if(InfoOffButton != null )
        {
            InfoOffButton.onClick.AddListener(InfoOff);
        }
    }

    private void InfoOff()
    {
        InfoOffButton.gameObject.SetActive(false);
        GameManager.isOpenInfoUI = false;
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
