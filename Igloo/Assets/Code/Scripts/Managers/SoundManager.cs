using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum Sound
{
    Bgm,
    Effect,
    Effect2,
    MaxCount
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [HideInInspector]
    public float bgmVolume = -6.0f;
    [HideInInspector]
    public float sfxVolume = -6.0f;

    AudioSource[] audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip> ();

    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioMixer audioMixer;

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
        string[] soundNames = System.Enum.GetNames(typeof(Sound)); // "Bgm", "Effect", "Effect2"
        for (int i = 0; i < soundNames.Length - 1; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = this.transform;
        }

        audioSources[(int)Sound.Bgm].loop = true; // bgm ������ ���� �ݺ� ���
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups("Master");
        foreach(AudioMixerGroup group in groups)
        {
            if(group.name == "BGM")
            {
                audioSources[(int)Sound.Bgm].outputAudioMixerGroup = group;
            }
            if(group.name == "SFX")
            {
                audioSources[(int)Sound.Effect].outputAudioMixerGroup = group;
            }
        }
        


        Play(bgmClip, Sound.Bgm, 1, 0.1f);

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

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float pitch = 1.0f, float volume = 0.5f)
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
            audioSource.volume = volume;
            audioSource.Play();
        }
        else // Effect ȿ���� ���
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void ChangeBgmSpeed(float value)
    {
        audioSources[(int)Sound.Bgm].pitch = value;
    }

    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f, float volume = 0.5f)
    {
        AudioClip audioClip = GetorAddAudioClip(path, type);
        Play(audioClip, type, pitch, volume);
    }

    AudioClip GetorAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }


        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }

        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
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
