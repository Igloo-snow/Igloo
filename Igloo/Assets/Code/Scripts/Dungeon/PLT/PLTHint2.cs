using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLTHint2 : Interactable
{
    [SerializeField] private GameObject hint;
    public GameObject itemPrefab;
    bool check;

    void Start()
    {
        promptMessage = "[E] 설명 보기";
        check = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        hint.SetActive(true);
        promptMessage = "[E] 설명 보기";

        if (check == false)
        {
            GameObject instance = Instantiate(itemPrefab);
        }

        check = true;
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
