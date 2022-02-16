using System.Collections.Generic;
using UnityEngine;

public class AudioSourcesSetterByTags : MonoBehaviour
{
    public enum NoTagSetting
    {
        Bgm,
        Sfx,
        Ambient,
    }

    [System.Serializable]
    public struct CustomIdTag
    {
        public string tag;
        public string id;
    }

    public NoTagSetting noTagSetting = NoTagSetting.Sfx;
    public string bgmTag = "BGM";
    public string sfxTag = "SFX";
    public string ambientTag = "AMBIENT";
    public CustomIdTag[] customIdTags = new CustomIdTag[0];

    private void Start()
    {
        Dictionary<string, string> tempCustomIdTags = new Dictionary<string, string>();
        foreach (CustomIdTag customIdTag in customIdTags)
        {
            tempCustomIdTags[customIdTag.tag] = customIdTag.id;
        }
        AudioSourceSetter tempComp;
        AudioSource[] sources = FindObjectsOfType<AudioSource>(true);
        bool tempIsSourcePlaying;
        foreach (AudioSource source in sources)
        {
            tempComp = source.gameObject.GetComponent<AudioSourceSetter>();
            if (tempComp) continue;
            tempComp = source.gameObject.AddComponent<AudioSourceSetter>();
            tempComp.playOnAwake = source.playOnAwake;
            tempIsSourcePlaying = source.isPlaying;
            if (tempComp.gameObject.CompareTag(bgmTag))
            {
                tempComp.type = AudioComponentSettingType.Bgm;
            }
            else if (tempComp.gameObject.CompareTag(sfxTag))
            {
                tempComp.type = AudioComponentSettingType.Sfx;
            }
            else if (tempComp.gameObject.CompareTag(ambientTag))
            {
                tempComp.type = AudioComponentSettingType.Ambient;
            }
            else if (tempCustomIdTags.ContainsKey(tempComp.gameObject.tag))
            {
                tempComp.type = AudioComponentSettingType.Other;
                tempComp.otherSettingId = tempCustomIdTags[tempComp.gameObject.tag];
            }
            else
            {
                switch (noTagSetting)
                {
                    case NoTagSetting.Bgm:
                        tempComp.type = AudioComponentSettingType.Bgm;
                        break;
                    case NoTagSetting.Sfx:
                        tempComp.type = AudioComponentSettingType.Sfx;
                        break;
                    case NoTagSetting.Ambient:
                        tempComp.type = AudioComponentSettingType.Ambient;
                        break;
                }
            }
            if (!tempIsSourcePlaying)
                tempComp.Stop();
        }
    }
}
