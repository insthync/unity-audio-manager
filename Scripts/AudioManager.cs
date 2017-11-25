using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        VolumeSettings[masterVolumeSetting.id] = masterVolumeSetting;
        VolumeSettings[bgmVolumeSetting.id] = bgmVolumeSetting;
        VolumeSettings[sfxVolumeSetting.id] = sfxVolumeSetting;
        VolumeSettings[ambientVolumeSetting.id] = ambientVolumeSetting;
        foreach (var otherVolumeSetting in otherVolumeSettings)
        {
            VolumeSettings[otherVolumeSetting.id] = otherVolumeSetting;
        }
    }
}
