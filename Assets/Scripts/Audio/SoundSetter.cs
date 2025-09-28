using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SoundSetter : MonoBehaviour
{
    private readonly string MasterVolume = "Master";
    private readonly string AmbientVolume = "Ambient";
    private readonly string EffectVolume = "Effect";

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderAmbientVolume;
    [SerializeField] private Slider _sliderEffectVolume;
    [SerializeField] private Button _buttonToggleVolume;

    private AudioSettingsData _settings;

    private void Start()
    {
        if (_buttonToggleVolume != null)
        {
            _buttonToggleVolume.onClick.AddListener(ToggleMusic);
        }

        InitializeSlider(_sliderMasterVolume, MasterVolume);
        InitializeSlider(_sliderAmbientVolume, AmbientVolume);
        InitializeSlider(_sliderEffectVolume, EffectVolume);
    }

    [Inject]
    public void Construct(AudioSettingsData settings)
    {
        _settings = settings;
    }

    private void InitializeSlider(Slider slider, string parameter)
    {
        if (_mixer.GetFloat(parameter, out float currentVolume))
        {
            slider.SetValueWithoutNotify(Mathf.Pow(10, currentVolume / 20));
        }

        slider.onValueChanged.AddListener(volume =>
            OnChangedVolume(volume, parameter));
    }

    private void ToggleMusic()
    {
        _settings.SetMute(!_settings.IsMuted);

        if (_settings.IsMuted)
        {
            _mixer.SetFloat(MasterVolume, -80f);
            _mixer.SetFloat(AmbientVolume, -80f);
            _mixer.SetFloat(EffectVolume, -80f);
        }
        else
        {
            OnChangedVolume(_settings.MasterVolume, MasterVolume);
            OnChangedVolume(_settings.AmbientVolume, AmbientVolume);
            OnChangedVolume(_settings.EffectVolume, EffectVolume);
        }
    }

    private void OnChangedVolume(float volume, string parameter)
    {
        if (_settings.IsMuted) 
            return;

        float currentVolume = Mathf.Log10(volume) * 20;

        if (parameter == MasterVolume)
            _settings.SetMasterVolume(volume);
        else if (parameter == AmbientVolume)
            _settings.SetAmbientVolume(volume);
        else if (parameter == EffectVolume)
            _settings.SetEffectVolume(volume);

        _mixer.SetFloat(parameter, volume > 0 ? currentVolume : -80f);
    }
}
