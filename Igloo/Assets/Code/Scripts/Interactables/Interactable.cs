using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptMessage;

    // will be called from player
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
