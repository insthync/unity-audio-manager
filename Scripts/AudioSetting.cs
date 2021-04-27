using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AudioSetting
{
    public const string KeyVolumeOnPrefix = "SETTING_VOLUME_ON_";
    public const string KeyVolumeLevelPrefix = "SETTING_VOLUME_LEVEL_";
    public string id;
    [FormerlySerializedAs("maxVolumeRate")]
    [Range(0.01f, 1f)]
    public float volumeScale = 1f;

    private bool? isOn;
    private float? levelSetting;

    public bool IsOn
    {
        get
        {
            if (!isOn.HasValue)
                isOn = PlayerPrefs.GetInt(KeyVolumeOnPrefix + id, 1) == 0 ? false : true;
            return isOn.Value;
        }
        set
        {
            isOn = value;
            PlayerPrefs.SetInt(KeyVolumeOnPrefix + id, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public float LevelSetting
    {
        get
        {
            if (!levelSetting.HasValue)
                levelSetting = PlayerPrefs.GetFloat(KeyVolumeLevelPrefix + id, 1);
            return levelSetting.Value;
        }
        set
        {
            levelSetting = value;
            PlayerPrefs.SetFloat(KeyVolumeLevelPrefix + id, value);
            PlayerPrefs.Save();
        }
    }

    public float Level
    {
        get
        {
            if (!IsOn)
                return 0;
            return LevelSetting * volumeScale;
        }
    }
}
