using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook playerCamera;
    public CinemachineVirtualCamera[] virtualCameras;

    public PlayerMovement playerMovement;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera == targetCamera;
        }
    }
}
