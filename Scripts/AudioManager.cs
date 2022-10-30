using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_singleton;
    public static AudioManager Singleton
    {
        get
        {
            if (s_singleton != null)
                return s_singleton;
            return new GameObject("_AudioManager").AddComponent<AudioManager>();
        }
        private set
        {
            s_singleton = value;
        }
    }

    public AudioSetting masterVolumeSetting = new AudioSetting() { id = "MASTER" };
    public AudioSetting bgmVolumeSetting = new AudioSetting() { id = "BGM" };
    public AudioSetting sfxVolumeSetting = new AudioSetting() { id = "SFX" };
    public AudioSetting ambientVolumeSetting = new AudioSetting() { id = "AMBIENT" };
    public AudioSetting[] otherVolumeSettings;

    public Dictionary<string, AudioSetting> VolumeSettings { get; private set; } = new Dictionary<string, AudioSetting>();

    private void Awake()
    {
        if (s_singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        s_singleton = this;
        DontDestroyOnLoad(gameObject);

        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
            AudioListener.pause = true;

        VolumeSettings[masterVolumeSetting.id] = masterVolumeSetting;
        VolumeSettings[bgmVolumeSetting.id] = bgmVolumeSetting;
        VolumeSettings[sfxVolumeSetting.id] = sfxVolumeSetting;
        VolumeSettings[ambientVolumeSetting.id] = ambientVolumeSetting;
        foreach (AudioSetting otherVolumeSetting in otherVolumeSettings)
        {
            VolumeSettings[otherVolumeSetting.id] = otherVolumeSetting;
        }
    }

    public void SetVolumeIsOn(string id, bool isOn)
    {
        if (VolumeSettings.ContainsKey(id))
            VolumeSettings[id].IsOn = isOn;
    }

    public bool GetVolumeIsOn(string id)
    {
        if (VolumeSettings.ContainsKey(id))
            return VolumeSettings[id].IsOn;
        return false;
    }

    public float GetVolumeLevel(string id)
    {
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
            return 0;

        if (VolumeSettings.ContainsKey(id))
            return VolumeSettings[id].Level;
        return 0;
    }

    public void SetVolumeLevelSetting(string id, float level)
    {
        if (VolumeSettings.ContainsKey(id))
            VolumeSettings[id].LevelSetting = level;
    }

    public float GetVolumeLevelSetting(string id)
    {
        if (VolumeSettings.ContainsKey(id))
            return VolumeSettings[id].LevelSetting;
        return 0;
    }

    public static void PlaySfxClipAtPoint(AudioClip audioClip, Vector3 position, float volumeScale = 1f)
    {
        if (Application.isBatchMode || AudioListener.pause || audioClip == null) return;
        AudioSource.PlayClipAtPoint(audioClip, position, (Singleton == null ? 1f : Singleton.sfxVolumeSetting.Level) * volumeScale);
    }

    public static void PlaySfxClipAtAudioSource(AudioClip audioClip, AudioSource audioSource, float volumeScale = 1f)
    {
        if (Application.isBatchMode || AudioListener.pause || audioClip == null || audioSource == null) return;
        audioSource.PlayOneShot(audioClip, (Singleton == null ? 1f : Singleton.sfxVolumeSetting.Level) * volumeScale);
    }
}
