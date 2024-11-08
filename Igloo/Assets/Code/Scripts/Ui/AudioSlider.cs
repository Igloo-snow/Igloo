using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;
    public string groupName;

    private void OnEnable()
    {
        InitSlider();
    }

    void InitSlider()
    {
        float thisValue = 0;
        if (groupName == "BGM")
        {
            Debug.Log("bgm");
            thisValue = SoundManager.instance.bgmVolume;
        }
        else if (groupName == "SFX")
        {
            Debug.Log("sfx");
            thisValue = SoundManager.instance.sfxVolume;
        }
        Debug.Log("this  " + thisValue);
        
        slider.value = Mathf.Pow(10, thisValue / 20);
        Debug.Log("slider :  " + slider.value);
        
    }

    public void SetLevel(float sliderValue)
    {
        float value = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat(groupName, value);
        if(groupName == "BGM")
        {
            SoundManager.instance.bgmVolume = value;
        }
        else if (groupName == "SFX")
        {
            SoundManager.instance.sfxVolume = value;
        }
    }
}
