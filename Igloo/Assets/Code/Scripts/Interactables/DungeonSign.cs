using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSign : Interactable
{
    [SerializeField] private GameObject sign;

    void Start()
    {
        promptMessage = "E키를 눌러 확인해보자";
    }

    protected override void Interact()
    {
        sign.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            sign.SetActive(false);
        }
    }
}
