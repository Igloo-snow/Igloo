using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private InformationUI infoUI;
    private bool triggerEntered;
    private Interactable interactable;

    void Start()
    {
        infoUI = GetComponent<InformationUI>();
        triggerEntered = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerEntered)
        {
            interactable.BaseInteract();
            infoUI.UpdateText(interactable.promptMessage);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            interactable = collider.GetComponent<Interactable>();
            infoUI.UpdateText(interactable.promptMessage);
            triggerEntered = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            infoUI.UpdateText("");
            triggerEntered = false;
        }
    }
}
