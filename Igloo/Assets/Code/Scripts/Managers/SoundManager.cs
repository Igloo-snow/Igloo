using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Sound
{
    Bgm,
    Effect,
    MaxCount
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource[] audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip> ();

    [SerializeField] AudioClip bgmClip; 

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    public void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(Sound)); // "Bgm", "Effect"
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = this.transform;
        }

        audioSources[(int)Sound.Bgm].loop = true; // bgm ������ ���� �ݺ� ���

        Play(bgmClip, Sound.Bgm);

    }

    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ���� Dictionary ����
        audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm) // BGM ������� ���
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
        else // Effect ȿ���� ���
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void RepeatPlayEffect(AudioClip audioClip)
    {
        audioSources[(int)Sound.Effect].loop = true;
        AudioSource audioSource = audioSources[(int)Sound.Effect];
        audioSource.pitch = 1.0f;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StopRepeatingEffect()
    {
        audioSources[(int)Sound.Effect].loop = false;
        audioSources[(int)Sound.Effect].Stop();
    }

}
