using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.UI.SoundView;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.Source.Scripts.Audio
{
    public class SoundSetter : MonoBehaviour, IObjectInitilizable
    {
        private const string Master = "Master";
        private const string Ambient = "Ambient";
        private const string Effect = "Effect";
        private const float MinVolume = 0.0001f;
        private const float MaxVolume = 1f;
        private const float VolumeMultiplier = 20f;
        private const float MuteDB = -80f;

        [SerializeField] private AudioMixer _mixer;

        private AudioSettingsData _settings;
        private VolumeSliderViewHandler _volumeSliderView;

        public bool IsInitialized { get; private set; }

        [Inject]
        public void Construct(AudioSettingsData settings)
        {
            _settings = settings ??
                throw new ArgumentNullException(nameof(settings),
                "[SoundSetter] AudioSettingsData не может быть null");
        }

        public void Initialize()
        {
            ValidateDependencies();

            _volumeSliderView.OnMasterChanged += 
                volume => SetVolume(Master, volume);
            _volumeSliderView.OnAmbientChanged += 
                volume => SetVolume(Ambient, volume);
            _volumeSliderView.OnEffectChanged += 
                volume => SetVolume(Effect, volume);
            _volumeSliderView.OnMuteClicked += OnToggleMute;

            ValidateSettingsValues();
            _volumeSliderView.SetInitialValues(
                _settings.Master,
                _settings.Ambient,
                _settings.Effect,
                _settings.IsMuted
            );
            RestoreVolumes();

            IsInitialized = true;
        }

        public void ApplyAudioHandler(VolumeSliderViewHandler volumeSliderView)
        {
            _volumeSliderView = volumeSliderView ??
                throw new ArgumentNullException(nameof(volumeSliderView),
                "[SoundSetter] Панель не может быть null");
        }

        private void SetVolume(string parameter, float value)
        {
            if (_settings.IsMuted)
                return;

            float db = Mathf.Log10(Mathf.Clamp(value, MinVolume, MaxVolume)) * VolumeMultiplier;

            _mixer.SetFloat(parameter, db);

            switch (parameter)
            {
                case Master: _settings.SetMasterVolume(value); break;
                case Ambient: _settings.SetAmbientVolume(value); break;
                case Effect: _settings.SetEffectVolume(value); break;
            }
        }

        private void OnToggleMute()
        {
            _settings.ChangeMuteState();

            if (_settings.IsMuted)
                MuteAll();
            else
                RestoreVolumes();
        }

        private void MuteAll()
        {
            _mixer.SetFloat(Master, MuteDB);
            _mixer.SetFloat(Ambient, MuteDB);
            _mixer.SetFloat(Effect, MuteDB);
        }

        private void RestoreVolumes()
        {
            SetVolume(Master, _settings.Master);
            SetVolume(Ambient, _settings.Ambient);
            SetVolume(Effect, _settings.Effect);
        }

        private void ValidateSettingsValues()
        {
            ValidateVolumeValue(_settings.Master, Master);
            ValidateVolumeValue(_settings.Ambient, Ambient);
            ValidateVolumeValue(_settings.Effect, Effect);
        }

        private void ValidateDependencies()
        {
            if (_mixer == null)
                throw new InvalidOperationException("[SoundSetter] AudioMixer не установлен в инспекторе");

            if (_settings == null)
                throw new InvalidOperationException("[SoundSetter] AudioSettingsData не был передан в Construct()");

            if (_volumeSliderView == null)
                throw new InvalidOperationException("[SoundSetter] VolumeSliderViewHandler не был передан в ApplyAudioHandler()");
        }

        private void ValidateVolumeValue(float volume, string name)
        {
            if (volume < 0f || volume > MaxVolume)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(volume), $"[SoundSetter] Значение громкости '{name}' " +
                    $"должно быть в диапазоне 0..1, получено: {volume}");
            }
        }
    }
}