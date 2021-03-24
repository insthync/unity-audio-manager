using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton { get; private set; }

    public AudioSetting masterVolumeSetting = new AudioSetting() { id = "MASTER" };
    public AudioSetting bgmVolumeSetting = new AudioSetting() { id = "BGM" };
    public AudioSetting sfxVolumeSetting = new AudioSetting() { id = "SFX" };
    public AudioSetting ambientVolumeSetting = new AudioSetting() { id = "AMBIENT" };
    public AudioSetting[] otherVolumeSettings;

    private readonly Dictionary<string, AudioSetting> VolumeSettings = new Dictionary<string, AudioSetting>();

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;
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

    public static void PlaySfxClipAtPoint(AudioClip audioClip, Vector3 position)
    {
        if (audioClip == null) return;
        AudioSource.PlayClipAtPoint(audioClip, position, Singleton == null ? 1f : Singleton.sfxVolumeSetting.Level);
    }

    public static void PlaySfxClipAtAudioSource(AudioClip audioClip, AudioSource audioSource)
    {
        if (audioClip == null || audioSource == null) return;
        audioSource.PlayOneShot(audioClip, Singleton == null ? 1f : Singleton.sfxVolumeSetting.Level);
    }
}
