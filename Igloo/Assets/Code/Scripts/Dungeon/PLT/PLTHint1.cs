using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLTHint1 : Interactable
{
    [SerializeField] private GameObject hint;


    void Start()
    {
        promptMessage = "[E] 설명 보기";
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        hint.SetActive(true);
        promptMessage = "[E] 설명 보기";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            hint.SetActive(false);
        }
        promptMessage = "[E] 설명 보기";
    }
}
