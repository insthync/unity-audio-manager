using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class AudioSettingManager : MonoBehaviour
{
    private Dictionary<string, AudioSetting> defaultVolumeSettings = new Dictionary<string, AudioSetting>();
    private Dictionary<string, AudioSlider> sliders = new Dictionary<string, AudioSlider>();
    private Dictionary<string, AudioToggle> toggles = new Dictionary<string, AudioToggle>();

    private void OnEnable()
    {
        defaultVolumeSettings.Clear();
        foreach (KeyValuePair<string, AudioSetting> kv in AudioManager.Singleton.VolumeSettings)
        {
            defaultVolumeSettings[kv.Key] = kv.Value.Clone();
        }
        AudioSlider[] sliderComps = GetComponentsInChildren<AudioSlider>(true);
        foreach (AudioSlider sliderComp in sliderComps)
        {
            sliders[sliderComp.SettingId] = sliderComp;
        }
        AudioToggle[] toggleComps = GetComponentsInChildren<AudioToggle>(true);
        foreach (AudioToggle toggleComp in toggleComps)
        {
            toggles[toggleComp.SettingId] = toggleComp;
        }
    }

    public void ResetAudioSetting()
    {
        foreach (var kv in defaultVolumeSettings)
        {
            if (sliders.ContainsKey(kv.Key))
                sliders[kv.Key].slider.SetValueWithoutNotify(kv.Value.LevelSetting);
            if (toggles.ContainsKey(kv.Key))
                toggles[kv.Key].toggle.SetIsOnWithoutNotify(kv.Value.IsOn);
            AudioManager.Singleton.VolumeSettings[kv.Key] = kv.Value.Clone();
        }
    }
}
