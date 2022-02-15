using System.Collections.Generic;
using UnityEngine;

public class AudioSettingManager : MonoBehaviour
{
    private Dictionary<string, AudioSetting> defaultVolumeSettings = new Dictionary<string, AudioSetting>();

    private void OnEnable()
    {
        defaultVolumeSettings.Clear();
        foreach (var kv in AudioManager.Singleton.VolumeSettings)
        {
            defaultVolumeSettings[kv.Key] = kv.Value;
        }
    }

    public void ResetAudioSetting()
    {
        foreach (var kv in defaultVolumeSettings)
        {
            AudioManager.Singleton.VolumeSettings[kv.Key] = kv.Value;
        }
    }
}
