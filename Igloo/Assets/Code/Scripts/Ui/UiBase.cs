using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class UiBase : MonoBehaviour
{
    public bool isOpen { get; private set; }
    [SerializeField] private bool isStaticUi;
    public bool IsStaticUi { get {return isStaticUi; } }
    [SerializeField] private bool isSingleUi;
    public bool IsSingleUi { get { return isSingleUi; } }
    public AudioClip audioClip;


    public void OpenUi()
    {
        this.gameObject.SetActive(true);
        isOpen = true;
    }

    public void CloseUi()
    {
        this.gameObject.SetActive(false);
        isOpen = false;
    }


}
