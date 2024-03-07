using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Phone : Interactable
{
    [SerializeField] private Image phoneUi;
    [SerializeField] private AudioSource phoneAudioSource;
    [SerializeField] private BlinkingObject visualEffectPrefab;
    private BlinkingObject visualEffect;
    private bool isFinish;

    protected override void Interact()
    {
        phoneUi.gameObject.SetActive(true);
        GameManager.isOpenUI = true;
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
        phoneUi.gameObject.SetActive(false);
        GameManager.isOpenUI = false;
    }

    public void Finish()
    {
        phoneUi.gameObject.SetActive(false);
        GameManager.isOpenUI = false;
        visualEffect.StopBlinking();
        StopSound();
        StopAllCoroutines();
    }
}
