using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : Interactable
{
    [SerializeField] private Sprite img;
    [SerializeField] private Image container;
    
    void Start()
    {
        promptMessage = "E키를 눌러 확인해보자";    
    }

    protected override void Interact()
    {
        container.sprite = img;
        container.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            container.gameObject.SetActive(false);

        }
    }
}
