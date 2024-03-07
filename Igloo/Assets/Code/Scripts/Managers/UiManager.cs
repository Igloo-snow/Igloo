using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    //UI 관련 상태 
    public static bool isStaticUiOpen = false;
    public static bool isDynamicUiOpen = false;
    public static bool isOpenUI = false;

    private UiBase currentOpenUi = new UiBase();
    private UiBase newUi;

    public AudioClip audioClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStaticUiOpen)
        {
            GameEventsManager.instance.playerEvents.PlayerStop();
        }
        else
        {
            GameEventsManager.instance.playerEvents.PlayerStart();
        }
    }

    public void CheckUi(UiBase go)
    {
        newUi = go;

        if (!currentOpenUi.isOpen)
        {
            newUi.OpenUi();
            PlaySfx();
            currentOpenUi = newUi;
        }
        else if (currentOpenUi == newUi)
        {
            currentOpenUi.CloseUi();
            PlaySfx();
        }
        else
        {
            TryOpenDubbleUi();
        }

        CheckStatic();

    }

    private void CheckStatic()
    {
        if (currentOpenUi.IsStaticUi)
            isStaticUiOpen = currentOpenUi.isOpen;
    }

    private void TryOpenDubbleUi()
    {
        if (newUi.IsSingleUi || currentOpenUi.IsSingleUi)
        {
            SoundManager.instance.Play("033_Denied_03");
            return;
        }
        else
        {
            currentOpenUi.CloseUi();
            newUi.OpenUi();
            PlaySfx();
            currentOpenUi = newUi;
            Debug.Log("2" + currentOpenUi.name);

        }
    }

    private void PlaySfx()
    {
        if (newUi.audioClip != null)
        {
            SoundManager.instance.Play(newUi.audioClip);
        }
        else if (audioClip != null)
        {
            SoundManager.instance.Play(audioClip);
        }
    }
}

