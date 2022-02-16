using UnityEngine;

public class AudioSourceSetterWithoutControls : AudioComponent
{
    private AudioSource cacheAudioSource;
    private int dirtyVolume;

    private void Awake()
    {
        cacheAudioSource = GetComponent<AudioSource>();
        if (cacheAudioSource == null)
        {
            cacheAudioSource = gameObject.AddComponent<AudioSource>();
            cacheAudioSource.spatialBlend = 1f;
        }
    }

    private void Update()
    {
        float volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
        int intVolume = (int)(volume * 100);
        if (dirtyVolume != intVolume)
        {
            dirtyVolume = intVolume;
            cacheAudioSource.volume = volume;
        }
    }
}
