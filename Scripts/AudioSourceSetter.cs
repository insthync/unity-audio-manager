using UnityEngine;

public class AudioSourceSetter : AudioComponent
{
    public enum PlayMode
    {
        PlayClipAtAudioSource,
        PlayClipAtPoint,
    }
    public PlayMode playMode = PlayMode.PlayClipAtAudioSource;
    public bool playOnAwake = true;
    public bool playOnEnable = false;
    public AudioClip[] randomClips = new AudioClip[0];
    private AudioSource cacheAudioSource;
    private bool isAwaken;
    private int dirtyVolume;

    private void Awake()
    {
        if (playMode == PlayMode.PlayClipAtAudioSource)
        {
            cacheAudioSource = GetComponent<AudioSource>();
            if (cacheAudioSource == null)
            {
                cacheAudioSource = gameObject.AddComponent<AudioSource>();
                cacheAudioSource.spatialBlend = 1f;
            }
            cacheAudioSource.Stop();
        }

        if (playOnAwake)
            Play();

        isAwaken = true;
    }

    private void OnEnable()
    {
        if (playOnEnable && isAwaken)
            Play();
    }

    public void Play()
    {
        if (AudioListener.pause)
            return;
        AudioClip clip = null;
        if (randomClips.Length > 0)
            clip = randomClips[Random.Range(0, randomClips.Length)];

        // No random clips, try to use clip from audio source
        if (clip == null)
        {
            if (cacheAudioSource.clip == null)
                return;
            clip = cacheAudioSource.clip;
        }

        float volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
        switch (playMode)
        {
            case PlayMode.PlayClipAtAudioSource:
                cacheAudioSource.clip = clip;
                cacheAudioSource.volume = volume;
                cacheAudioSource.Play();
                break;
            case PlayMode.PlayClipAtPoint:
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                break;
        }
    }

    public void Pause()
    {
        if (playMode != PlayMode.PlayClipAtAudioSource)
            return;
        cacheAudioSource.Pause();
    }

    public void UnPause()
    {
        if (playMode != PlayMode.PlayClipAtAudioSource)
            return;
        cacheAudioSource.UnPause();
    }

    public void Stop()
    {
        if (playMode != PlayMode.PlayClipAtAudioSource)
            return;
        cacheAudioSource.Stop();
    }

    private void Update()
    {
        float volume = AudioManager.Singleton.GetVolumeLevel(SettingId);
        int intVolume = (int)(volume * 100);
        if (playMode == PlayMode.PlayClipAtAudioSource && dirtyVolume != intVolume)
        {
            dirtyVolume = intVolume;
            cacheAudioSource.volume = volume;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.volume = 0;
        }
    }
#endif
}
