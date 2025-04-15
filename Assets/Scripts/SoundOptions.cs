using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    private const string s_masterExposedName = "MasterVolume";
    private const string s_effectsExposedName = "EffectsVolume";
    private const string s_musicExposedName = "MusicVolume";

    [SerializeField] private AudioMixerGroup _mixer;

    public void ToggleMasterVolume(bool isEnabled)
    {
        ToggleVolume(s_masterExposedName, isEnabled);
    }

    public void ToggleEffectsVolume(bool isEnabled)
    {
        ToggleVolume(s_effectsExposedName, isEnabled);
    }

    public void ToggleMusicVolume(bool isEnabled)
    {
        ToggleVolume(s_musicExposedName, isEnabled);
    }

    public void ChangeMasterVolume(float volume)
    {
        ChangeVolume(s_masterExposedName, volume);
    }

    public void ChangeEffectsVolume(float volume)
    {
        ChangeVolume(s_effectsExposedName, volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        ChangeVolume(s_musicExposedName, volume);
    }

    private void ToggleVolume(string exposedName, bool isEnabled)
    {
        if (isEnabled)
            _mixer.audioMixer.SetFloat(exposedName, 0);
        else
            _mixer.audioMixer.SetFloat(exposedName, -80);
    }

    public void ChangeVolume(string exposedName, float volume)
    {
        _mixer.audioMixer.SetFloat(exposedName, Mathf.Log10(volume) * 20);
    }
}
