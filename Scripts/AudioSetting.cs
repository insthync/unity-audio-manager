using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSetting
{
    public const string KeyVolumeOnPrefix = "SETTING_VOLUME_ON_";
    public const string KeyVolumeLevelPrefix = "SETTING_VOLUME_LEVEL_";
    public string id;
    [Range(0.01f, 1f)]
    public float maxVolumeRate = 1f;

    public bool IsOn
    {
        get { return PlayerPrefs.GetInt(KeyVolumeOnPrefix + id, 1) == 0 ? false : true; }
        set
        {
            PlayerPrefs.SetInt(KeyVolumeOnPrefix + id, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public float Level
    {
        get
        {
            if (!IsOn)
                return 0;
            return LevelSetting * maxVolumeRate;
        }
    }

    public float LevelSetting
    {
        get
        {
            return PlayerPrefs.GetFloat(KeyVolumeLevelPrefix + id, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(KeyVolumeLevelPrefix + id, value);
            PlayerPrefs.Save();
        }
    }
}
