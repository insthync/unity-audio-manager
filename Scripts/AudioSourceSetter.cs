using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceSetter : AudioComponent
{
    public enum PlayMode
    {
        PlayClipAtAudioSource,
        PlayClipAtPoint,
    }
    public PlayMode playMode;
    public bool playOnAwake = true;
    public AudioClip[] randomClips;
    private AudioSource tempAudioSource;
    public AudioSource TempAudioSource
    {
        get
        {
            if (tempAudioSource == null)
                tempAudioSource = GetComponent<AudioSource>();
            return tempAudioSource;
        }
    }
    private void Awake()
    {
        TempAudioSource.Stop();
        if (playOnAwake)
            Play();
    }

    public void Play()
    {
        AudioClip clip = null;
        if (randomClips.Length > 0)
            clip = randomClips[Random.Range(0, randomClips.Length - 1)];

        if (clip == null)
        {
            if (TempAudioSource.clip == null)
                return;
            clip = TempAudioSource.clip;
        }

        var volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
        switch (playMode)
        {
            case PlayMode.PlayClipAtAudioSource:
                TempAudioSource.clip = clip;
                TempAudioSource.volume = volume;
                TempAudioSource.Play();
                break;
            case PlayMode.PlayClipAtPoint:
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                break;
        }
    }

    private void Update()
    {
        TempAudioSource.volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        TempAudioSource.playOnAwake = false;
        TempAudioSource.volume = 0;
    }
#endif
}
