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

    public GameObject dragDropTest;
    public GameObject problemUI;

    void Start()
    {
        promptMessage = "";
        isMissionOn = false;
        dragDropTest.SetActive(false);
        problemUI.SetActive(false);

        targetCameras[0].enabled = false;
        targetCameras[1].enabled = false;
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
            dragDropTest.SetActive(false);
            problemUI.SetActive(false);
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
        dragDropTest.SetActive(true);
        problemUI.SetActive(true);
    }
}
