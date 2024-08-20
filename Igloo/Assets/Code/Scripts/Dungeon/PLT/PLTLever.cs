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
    public GameObject glass;
    public GameObject playerModel;

    public GameObject pot;
    public GameObject root;

    public AudioClip right;
    public AudioClip wrong;



    void Start()
    {
        promptMessage = "";
        isMissionOn = false;
        dragDropTest.SetActive(false);
        problemUI.SetActive(false);

        pot.SetActive(true);
        root.SetActive(false);

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

            glass.SetActive(true);
            playerModel.SetActive(true);

            pot.SetActive(true);
            root.SetActive(false);
        }
        else
        {
            targetCameras[0].enabled = true;
            playerMovement.enabled = false;

            playerModel.SetActive(false);

            Invoke("ActivateDragDrop", 2f);
            Invoke("SwitchActivateObjs", 4.5f);
        }
        isMissionOn = !isMissionOn;
    }

    private void ActivateDragDrop()
    {
        targetCameras[1].enabled = true;
        glass.SetActive(false);

    }

    private void SwitchActivateObjs()
    {
        dragDropTest.SetActive(true);
        problemUI.SetActive(true);

        pot.SetActive(false);
        root.SetActive(true);

    }

    public void RightAnswer()
    {
        SoundManager.instance.Play(right);
    }
}
