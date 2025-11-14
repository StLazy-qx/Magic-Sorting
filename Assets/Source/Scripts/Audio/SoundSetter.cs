using UnityEngine;
using UnityEngine.Audio;

public class SoundSetter
{
    private const string MasterVolume = "Master";
    private const string AmbientVolume = "Ambient";
    private const string EffectVolume = "Effect";
    private readonly AudioMixer _mixer;
    private readonly AudioSettingsData _settings;

    public bool IsMuted => _settings.IsMuted;

    public SoundSetter(AudioMixer mixer, AudioSettingsData settings)
    {
        _mixer = mixer;
        _settings = settings;
    }

    public void SetVolume(string parameter, float volume)
    {
        if (_settings.IsMuted)
            return;

        float dbValue = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        _mixer.SetFloat(parameter, dbValue);

        switch (parameter)
        {
            case MasterVolume:
                _settings.SetMasterVolume(volume);
                break;
            case AmbientVolume:
                _settings.SetAmbientVolume(volume);
                break;
            case EffectVolume:
                _settings.SetEffectVolume(volume);
                break;
        }
    }

    public float GetCurrentVolume(string parameter)
    {
        if (_mixer.GetFloat(parameter, out float dbValue))
            return Mathf.Pow(10f, dbValue / 20f);
        return 1f;
    }

    public void ToggleMute()
    {
        bool newMuteState = !_settings.IsMuted;
        _settings.SetMute(newMuteState);

        if (newMuteState)
        {
            MuteAll();
        }
        else
        {
            RestoreVolumes();
        }
    }

    private void MuteAll()
    {
        _mixer.SetFloat(MasterVolume, -80f);
        _mixer.SetFloat(AmbientVolume, -80f);
        _mixer.SetFloat(EffectVolume, -80f);
    }

    private void RestoreVolumes()
    {
        SetVolume(MasterVolume, _settings.MasterVolume);
        SetVolume(AmbientVolume, _settings.AmbientVolume);
        SetVolume(EffectVolume, _settings.EffectVolume);
    }

    //[SerializeField] private AudioMixer _mixer;
    //[SerializeField] private Slider _sliderMasterVolume;
    //[SerializeField] private Slider _sliderAmbientVolume;
    //[SerializeField] private Slider _sliderEffectVolume;
    //[SerializeField] private MuteButton _muteButton;

    //private AudioSettingsData _settings;

    //public bool IsInitialized { get; private set; }

    //public void Initilize()
    //{
    //    if (_muteButton != null)
    //    {
    //        _muteButton.OnClick.AddListener(ToggleMusic);
    //    }

    //    InitializeSlider(_sliderMasterVolume, MasterVolume);
    //    InitializeSlider(_sliderAmbientVolume, AmbientVolume);
    //    InitializeSlider(_sliderEffectVolume, EffectVolume);

    //    IsInitialized = true;
    //}

    //[Inject]
    //public void Construct(AudioSettingsData settings)
    //{
    //    _settings = settings;
    //}

    //private void InitializeSlider(Slider slider, string parameter)
    //{
    //    if (_mixer.GetFloat(parameter, out float currentVolume))
    //    {
    //        slider.SetValueWithoutNotify(Mathf.Pow(10, currentVolume / 20));
    //    }

    //    slider.onValueChanged.AddListener(volume =>
    //        OnChangedVolume(volume, parameter));
    //}

    //private void ToggleMusic()
    //{
    //    _settings.SetMute(_settings.IsMuted == false);

    //    if (_settings.IsMuted)
    //    {
    //        _mixer.SetFloat(MasterVolume, -80f);
    //        _mixer.SetFloat(AmbientVolume, -80f);
    //        _mixer.SetFloat(EffectVolume, -80f);
    //    }
    //    else
    //    {
    //        OnChangedVolume(_settings.MasterVolume, MasterVolume);
    //        OnChangedVolume(_settings.AmbientVolume, AmbientVolume);
    //        OnChangedVolume(_settings.EffectVolume, EffectVolume);
    //    }

    //    _muteButton.UpdateButtonColor(_settings.IsMuted);
    //}

    //private void OnChangedVolume(float volume, string parameter)
    //{
    //    if (_settings.IsMuted) 
    //        return;

    //    float currentVolume = Mathf.Log10(volume) * 20;

    //    if (parameter == MasterVolume)
    //        _settings.SetMasterVolume(volume);
    //    else if (parameter == AmbientVolume)
    //        _settings.SetAmbientVolume(volume);
    //    else if (parameter == EffectVolume)
    //        _settings.SetEffectVolume(volume);

    //    _mixer.SetFloat(parameter, volume > 0 ? currentVolume : -80f);
    //}
}
