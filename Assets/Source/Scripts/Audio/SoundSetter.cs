using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.UI.SoundView;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
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
        private const float SaveThreshold = 0.1f;
        private const int CheckIntervalMilliseconds = 500;

        [SerializeField] private AudioMixer _mixer;

        private AudioSettingsData _settings;
        private VolumeSliderViewHandler _volumeSliderView;
        private CancellationTokenSource _cancellationTokenSource;
        private float _masterIntervalStart;
        private float _ambientIntervalStart;
        private float _effectIntervalStart;

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

            _masterIntervalStart = _settings.Master;
            _ambientIntervalStart = _settings.Ambient;
            _effectIntervalStart = _settings.Effect;

            _cancellationTokenSource = new CancellationTokenSource();

            RunComparisonLoop(_cancellationTokenSource.Token).Forget();

            Application.quitting += OnApplicationQuitting;

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
                    _settings.UpdateMasterVolume(value);
                    break;
                case Ambient:
                    _settings.UpdateAmbientVolume(value);
                    break;
                case Effect:
                    _settings.UpdateEffectVolume(value);
                    break;

                    //case Master: 
                    //    _settings.SetMasterVolume(value);
                    //    break;

                    //case Ambient: 
                    //    _settings.SetAmbientVolume(value);
                    //    break;

                    //case Effect: 
                    //    _settings.SetEffectVolume(value);
                    //    break;
            }
        }

        private async UniTaskVoid RunComparisonLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    await UniTask.Delay(
                        CheckIntervalMilliseconds,
                        cancellationToken: cancellationToken);

                    CompareAndSaveVolumes();
                }
            }
            catch (OperationCanceledException) { }
        }

        private void CompareAndSaveVolumes()
        {
            CheckParameter(
                _settings.Master,
                _masterIntervalStart,
                value => _settings.SetMasterVolume(value),   // Сохранить, если порог превышен
                newValue => _masterIntervalStart = newValue); // Обновить переменную

            CheckParameter(
                _settings.Ambient,
                _ambientIntervalStart,
                value => _settings.SetAmbientVolume(value),
                newValue => _ambientIntervalStart = newValue);

            CheckParameter(
                _settings.Effect,
                _effectIntervalStart,
                value => _settings.SetEffectVolume(value),
                newValue => _effectIntervalStart = newValue);
        }

        private void CheckParameter(
            float currentValue,
            float intervalStart,
            Action<float> saveAction,
            Action<float> updateStartAction)
        {
            if (Mathf.Abs(currentValue - intervalStart) > SaveThreshold)
            {
                saveAction(currentValue);
            }

            updateStartAction(currentValue);
        }

        private void ForceSaveAudio()
        {
            YG2.saves.ForceSaveAudio();
        }

        private void SaveMasterOnRelease()
        {
            _settings.SetMasterVolume(_settings.Master);
            YG2.saves.ForceSaveAudio();
            _masterIntervalStart = _settings.Master; // Сбрасываем переменную, чтобы следующее сравнение было корректным
        }

        private void SaveAmbientOnRelease()
        {
            _settings.SetAmbientVolume(_settings.Ambient);
            YG2.saves.ForceSaveAudio();
            _ambientIntervalStart = _settings.Ambient;
        }

        private void SaveEffectOnRelease()
        {
            _settings.SetEffectVolume(_settings.Effect);
            YG2.saves.ForceSaveAudio();
            _effectIntervalStart = _settings.Effect;
        }

        private void OnApplicationQuitting()
        {
            _settings.SetMasterVolume(_settings.Master);
            _settings.SetAmbientVolume(_settings.Ambient);
            _settings.SetEffectVolume(_settings.Effect);
            YG2.saves.ForceSaveAudio();
        }

        private void OnDestroy()
        {
            Application.quitting -= OnApplicationQuitting;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        //

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