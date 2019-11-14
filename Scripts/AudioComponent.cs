using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioComponentSettingType
{
    Master,
    Bgm,
    Sfx,
    Ambient,
    Other
}

public class AudioComponent : MonoBehaviour
{
    public AudioComponentSettingType type;
    public string otherSettingId;

    public string SettingId
    {
        get
        {
            switch (type)
            {
                case AudioComponentSettingType.Master:
                    return AudioManager.Singleton.masterVolumeSetting.id;
                case AudioComponentSettingType.Bgm:
                    return AudioManager.Singleton.bgmVolumeSetting.id;
                case AudioComponentSettingType.Sfx:
                    return AudioManager.Singleton.sfxVolumeSetting.id;
                case AudioComponentSettingType.Ambient:
                    return AudioManager.Singleton.ambientVolumeSetting.id;
            }
            return otherSettingId;
        }
    }
}
