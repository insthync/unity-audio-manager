using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceSetter : AudioComponent
{
    public AudioSource audioSource { get; private set; }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
    }

    private void Update()
    {
        audioSource.volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
    }
}
