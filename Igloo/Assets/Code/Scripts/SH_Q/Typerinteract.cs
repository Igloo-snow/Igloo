using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typerinteract : MonoBehaviour
{
    public GameObject canvasUI;
    public float interactionDistance = 3f;

    void Start()
    {
        canvasUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                canvasUI.SetActive(true);
            }
        }
    }
}