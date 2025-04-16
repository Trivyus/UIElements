using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(SoundOptions))]
public class MasterSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private SoundOptions _soundOptions;

    private const string ExposedName = "MasterVolume";
    private const string SliderSaveSuffix = "Volume";

    private void Awake()
    {
        _soundOptions = GetComponent<SoundOptions>();
    }

    private void Start()
    {
        _soundOptions.LoadSettings(ParameterName());

        _slider.value = PlayerPrefs.GetFloat(ParameterName() + SliderSaveSuffix, 1f);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float linearValue)
    {
        _soundOptions.ChangeVolume(ParameterName(), linearValue);
    }

    private string ParameterName()
    {
        if (_mixerGroup != null)
            return _mixerGroup.name;

        return ExposedName;
    }
}
