using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Phone : Interactable
{
    public UiBase uiBase;
    [SerializeField] private AudioSource phoneAudioSource;
    [SerializeField] private BlinkingObject visualEffectPrefab;
    private BlinkingObject visualEffect;
    private bool isFinish;

    protected override void Interact()
    {
        UiManager.instance.CheckUi(uiBase);
    }

    public void PlaySound()
    {
        phoneAudioSource.Play();

    }

    public void StartVisualEffect()
    {
        visualEffect = Instantiate(visualEffectPrefab, this.transform);
        //visualEffect.StartBlinking();
    }
    
    public void StopSound()
    {
        phoneAudioSource.Stop();
    }

    public void Again()
    {
        UiManager.instance.CheckUi(uiBase);

    }

    public void Finish()
    {
        UiManager.instance.CheckUi(uiBase);
        visualEffect.StopBlinking();
        StopSound();
        StopAllCoroutines();
    }
}
