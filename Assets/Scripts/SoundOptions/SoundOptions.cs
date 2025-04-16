using UnityEngine;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    private const float MinVolumeDb = -80f;
    private const float DbConversion = 20f;
    private const float LinearBase = 10f;
    private const string SliderSaveSuffix = "Volume";
    private const string ToggleSaveSuffix = "Enabled";

    public void ToggleVolume(string exposedName, bool isEnabled)
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
            _mixer.SetFloat(exposedName, MinVolumeDb);
        }

        PlayerPrefs.SetInt(exposedName + ToggleSaveSuffix, isEnabled ? 1 : 0);
    }

    public void ChangeVolume(string exposedName, float volume)
    {
        float dbValue = Mathf.Log10(volume) * DbConversion;
        _mixer.SetFloat(exposedName, dbValue);

        if (PlayerPrefs.GetInt(exposedName + ToggleSaveSuffix, 1) == 1)
        {
            PlayerPrefs.SetFloat(exposedName + SliderSaveSuffix, volume);
        }
    }

    public float GetLinearVolume(string parameterName)
    {
        if (_mixer.GetFloat(parameterName, out float dbValue))
            return DbToLinear(dbValue);
        else
            return 0f;
    }

    private float DbToLinear(float db)
    {
        if (db <= MinVolumeDb)
            return 0f;
        else
            return Mathf.Pow(LinearBase, db / DbConversion);
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
            _mixer.SetFloat(exposedName, MinVolumeDb);
        }
    }
}
