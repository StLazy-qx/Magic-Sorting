using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.UI.SoundView;
using UnityEngine;
using UnityEngine.Audio;
using YG;
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

        [SerializeField] private AudioMixer _mixer;

        private AudioSettingsData _settings;
        private VolumeSliderViewHandler _volumeSliderView;

        public bool IsInitialized { get; private set; }

        [Inject]
        public void Construct(AudioSettingsData settings)
        {
            Guard.NotNull(settings, nameof(settings));

            _settings = settings;
        }

        public void Initialize()
        {
            ValidateDependencies();

            _settings.Load();

            _volumeSliderView.OnMasterChanged += 
                volume => SetVolume(Master, volume);
            _volumeSliderView.OnAmbientChanged += 
                volume => SetVolume(Ambient, volume);
            _volumeSliderView.OnEffectChanged += 
                volume => SetVolume(Effect, volume);

            _volumeSliderView.OnMasterReleased += ForceSaveAudio;
            _volumeSliderView.OnAmbientReleased += ForceSaveAudio;
            _volumeSliderView.OnEffectReleased += ForceSaveAudio;

            ValidateSettingsValues();
            _volumeSliderView.SetInitialValues(
                _settings.Master,
                _settings.Ambient,
                _settings.Effect
            );

            RestoreVolumes();

            IsInitialized = true;
        }

        public void ApplyAudioHandler(VolumeSliderViewHandler volumeSliderView)
        {
            Guard.NotNull(volumeSliderView, nameof(volumeSliderView));

            _volumeSliderView = volumeSliderView;
        }

        private void SetVolume(string parameter, float value)
        {
            float db = Mathf.Log10(Mathf.Clamp(
                value, MinVolume, MaxVolume)) * VolumeMultiplier;

            _mixer.SetFloat(parameter, db);

            switch (parameter)
            {
                case Master: 
                    _settings.SetMasterVolume(value);
                    break;

                case Ambient: 
                    _settings.SetAmbientVolume(value);
                    break;

                case Effect: 
                    _settings.SetEffectVolume(value);
                    break;
            }
        }

        private void ForceSaveAudio()
        {
            YG2.saves.ForceSaveAudio();
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
            Guard.NotNull(_mixer, nameof(_mixer));
            Guard.NotNull(_settings, nameof(_settings));
            Guard.NotNull(_volumeSliderView, nameof(_volumeSliderView));
        }

        private void ValidateVolumeValue(float volume, string name)
        {
            Guard.InRange(volume, 0f, MaxVolume, name);
        }
    }
}