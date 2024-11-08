using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingObject : Interactable
{
    bool isOn;
    public PlayerMovement playerMovement;
    public Vector3 offset;

    void Start()
    {
        promptMessage = "[E] ¾É±â";
        isOn = false;
    }


    void Update()
    {

    }

    protected override void Interact()
    {
        
        if (isOn)
        {
            playerMovement.enabled = true;
            playerMovement.Sit(false, gameObject.transform, Vector3.zero);
        }
        else
        {
            playerMovement.enabled = false;
            playerMovement.Sit(true, gameObject.transform, offset);
        }

        isOn = !isOn;
    }
}