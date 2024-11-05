using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLTHint1 : Interactable
{
    [SerializeField] private GameObject hint;


    void Start()
    {
        promptMessage = "[E] ���� ����";
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        hint.SetActive(true);
        promptMessage = "[E] ���� ����";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            hint.SetActive(false);
        }
        promptMessage = "[E] ���� ����";
    }
}
