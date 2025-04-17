using UnityEngine;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    private const float MinVolumeDecibels = -80f;
    private const float DecibelsConversion = 20f;
    private const float LinearBase = 10f;

    public const string SliderSaveSuffix = "Volume";
    public const string ToggleSaveSuffix = "Enabled";

    [SerializeField] private AudioMixerGroup _mixer;

    public void SwitchVolume(string exposedName, bool isEnabled)
    {
        if (isEnabled)
        {
            float savedVolume = PlayerPrefs.GetFloat(exposedName + SliderSaveSuffix, 1f);
            ChangeVolume(exposedName, savedVolume);
        }
        else
        {
            float currentVolume = GetLinearVolume(exposedName);
            PlayerPrefs.SetFloat(exposedName + SliderSaveSuffix, currentVolume);
            _mixer.audioMixer.SetFloat(exposedName, MinVolumeDecibels);
        }

        PlayerPrefs.SetInt(exposedName + ToggleSaveSuffix, isEnabled ? 1 : 0);
    }

    public void ChangeVolume(string exposedName, float volume)
    {
        float decibelsValue = Mathf.Log10(volume) * DecibelsConversion;
        _mixer.audioMixer.SetFloat(exposedName, decibelsValue);

        if (PlayerPrefs.GetInt(exposedName + ToggleSaveSuffix, 1) == 1)
        {
            PlayerPrefs.SetFloat(exposedName + SliderSaveSuffix, volume);
        }
    }

    public float GetLinearVolume(string parameterName)
    {
        if (_mixer.audioMixer.GetFloat(parameterName, out float decibelsValue))
            return ConvertDecibelsToLinear(decibelsValue);
        else
            return 0f;
    }

    public void LoadSettings(string exposedName)
    {
        bool isEnabled = PlayerPrefs.GetInt(exposedName + ToggleSaveSuffix, 1) == 1;

        if (isEnabled)
        {
            float savedVolume = PlayerPrefs.GetFloat(exposedName + SliderSaveSuffix, 1f);
            ChangeVolume(exposedName, savedVolume);
        }
        else
        {
            _mixer.audioMixer.SetFloat(exposedName, MinVolumeDecibels);
        }
    }

    private float ConvertDecibelsToLinear(float decibels)
    {
        if (decibels <= MinVolumeDecibels)
            return 0f;
        else
            return Mathf.Pow(LinearBase, decibels / DecibelsConversion);
    }
}
