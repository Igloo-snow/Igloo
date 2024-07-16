using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PLTLever : Interactable
{
    private bool isMissionOn;
    public CinemachineFreeLook playerCamera;
    public CinemachineVirtualCamera targetCamera;
    public PlayerMovement playerMovement;

    void Start()
    {
        promptMessage = "";
        isMissionOn = false;
    }

    void Update()
    {
        
    }

    protected override void Interact()
    {
        if (isMissionOn)
        {
            targetCamera.enabled = false;
            playerCamera.enabled = true;
            playerMovement.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
            targetCamera.enabled = true;
            playerMovement.enabled = false;
        }
        isMissionOn = !isMissionOn;
    }
}
