using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceSetter : AudioComponent
{
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
        TempAudioSource.volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
        if (randomClips.Length > 0)
            TempAudioSource.clip = randomClips[Random.Range(0, randomClips.Length - 1)];
        if (playOnAwake)
            TempAudioSource.Play();
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
