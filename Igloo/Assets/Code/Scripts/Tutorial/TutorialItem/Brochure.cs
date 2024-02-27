using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brochure : Interactable
{
    [SerializeField] private Image img;

    protected override void Interact()
    {
        img.gameObject.SetActive(true);
        GameManager.isOpenUI = true;
    }
}
