using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(SoundOptions))]
public class MasterToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private SoundOptions _soundOptions;
    private const string ExposedName = "MasterVolume";
    private const string ToggleSaveSuffix = "Enabled";

    private void Awake()
    {
        _soundOptions = GetComponent<SoundOptions>();
    }

    private void Start()
    {
        _soundOptions.LoadSettings(ParameterName());

        _toggle.isOn = PlayerPrefs.GetInt(ParameterName() + ToggleSaveSuffix, 1) == 1;
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        _soundOptions.ToggleVolume(ParameterName(), isOn);
    }

    private string ParameterName()
    {
        if (_mixerGroup != null)
            return _mixerGroup.name;

        return ExposedName;
    }
}