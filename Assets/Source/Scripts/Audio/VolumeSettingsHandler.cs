using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

public class VolumeSettingsHandler : MonoBehaviour, IObjectInitilizable
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderAmbientVolume;
    [SerializeField] private Slider _sliderEffectVolume;
    [SerializeField] private MuteButton _muteButton;

    private SoundSetter _soundSetter;
    private AudioSettingsData _settings;

    public bool IsInitialized { get; private set; }

    [Inject]
    public void Construct(AudioSettingsData settings)
    {
        _settings = settings;
    }

    public void Initilize()
    {
        _soundSetter = new SoundSetter(_mixer, _settings);

        InitializeSlider(_sliderMasterVolume, "Master");
        InitializeSlider(_sliderAmbientVolume, "Ambient");
        InitializeSlider(_sliderEffectVolume, "Effect");

        if (_muteButton != null)
            _muteButton.OnClick.AddListener(OnMuteButtonClicked);

        IsInitialized = true;
    }

    private void InitializeSlider(Slider slider, string parameter)
    {
        float currentValue = _soundSetter.GetCurrentVolume(parameter);
        slider.SetValueWithoutNotify(currentValue);

        slider.onValueChanged.AddListener(value =>
            _soundSetter.SetVolume(parameter, value));
    }

    private void OnMuteButtonClicked()
    {
        _soundSetter.ToggleMute();
        _muteButton.UpdateButtonColor(_settings.IsMuted);
    }
}
