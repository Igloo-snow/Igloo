using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSign : Interactable
{
    [SerializeField] private GameObject sign;

    void Start()
    {
        promptMessage = "EŰ�� ���� Ȯ���غ���";
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
