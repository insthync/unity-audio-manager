using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AudioText : AudioComponent
{
    public Text text { get; private set; }
    public string format = "{0}%";

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = string.Format(format, (AudioManager.Singleton.GetVolumeLevelSetting(SettingId) * 100f).ToString("N0"));
    }
}
