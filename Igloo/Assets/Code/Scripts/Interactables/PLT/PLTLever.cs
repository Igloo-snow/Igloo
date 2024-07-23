using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PLTLever : Interactable
{
    private bool isMissionOn;
    public CinemachineFreeLook playerCamera;
    public CinemachineVirtualCamera[] targetCameras;
    public PlayerMovement playerMovement;

    public GameObject DragDropTest;

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
            targetCameras[0].enabled = false;
            playerMovement.enabled = true;

            targetCameras[1].enabled = false;
            DragDropTest.SetActive(false);
        }
        else
        {
            targetCameras[0].enabled = true;
            playerMovement.enabled = false;

            Invoke("ActivateDragDrop", 2f);
            Invoke("ActivateObjs", 4.5f);
        }
        isMissionOn = !isMissionOn;
    }

    private void ActivateDragDrop()
    {
        targetCameras[1].enabled = true;
    }

    private void ActivateObjs()
    {
        DragDropTest.SetActive(true);
    }
}
