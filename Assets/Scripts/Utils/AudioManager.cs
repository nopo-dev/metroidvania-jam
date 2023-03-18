using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one AudioManager");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    public void PlayDelayedSound(string name, float delay)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayDelayed(delay);
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void FadeOut(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(FadeSound(s, duration, 0.05f));
    }

    public void FadeIn(string name, float duration)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        StartCoroutine(FadeSound(s, duration, 0.1f));
    }

    public static IEnumerator FadeSound(Sound s, float duration, float targetVolume)
    {
        float vol = s.source.volume;
        if (!s.source.isPlaying)
            yield break;
        float currentTime = 0;
        float start = s.source.volume;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            s.source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        s.source.Stop();
        s.source.volume = vol;
    }

    public Sound[] getSounds()
    {
        return sounds;
    }
}
