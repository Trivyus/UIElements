using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(SoundOptions))]
public class AudioMixerController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private SoundOptions _soundOptions;
    private string _exposedName;

    private void Awake()
    {
        _soundOptions = GetComponent<SoundOptions>();
        _exposedName = _mixerGroup.name;
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_exposedName + SoundOptions.SliderSaveSuffix, 1f);
        bool isMuted = PlayerPrefs.GetInt(_exposedName + SoundOptions.ToggleSaveSuffix, 1) == 0;

        _toggle.isOn = !isMuted;
        _slider.interactable = !isMuted;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderChanged);
        _toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
        _toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }

    private void OnSliderChanged(float linearValue)
    {
        _soundOptions.ChangeVolume(_exposedName, linearValue);
    }

    private void OnToggleChanged(bool isEnabled)
    {
        _soundOptions.SwitchVolume(_exposedName, isEnabled);
        _slider.interactable = isEnabled;

        if (isEnabled)
            _soundOptions.ChangeVolume(_exposedName, _slider.value);
    }
}
