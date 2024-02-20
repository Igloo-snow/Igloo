using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typerinteract : MonoBehaviour
{
    public GameObject specificUI;

    private bool isPlayerInRange;

    private void Awake()
    {
        isPlayerInRange = false;
        specificUI.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !specificUI.activeSelf)
            {
                specificUI.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
