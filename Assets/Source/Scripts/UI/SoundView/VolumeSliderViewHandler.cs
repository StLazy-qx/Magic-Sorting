using UnityEngine.UI;
using UnityEngine;
using System;

namespace Assets.Source.Scripts.UI.SoundView
{
    public class VolumeSliderViewHandler : MonoBehaviour
    {
        [SerializeField] private Slider _sliderMasterVolume;
        [SerializeField] private Slider _sliderAmbientVolume;
        [SerializeField] private Slider _sliderEffectVolume;
        [SerializeField] private MuteButton _muteButton;

        public event Action<float> OnMasterChanged;
        public event Action<float> OnAmbientChanged;
        public event Action<float> OnEffectChanged;
        public event Action OnMuteClicked;

        private void Awake()
        {
            ValidateInitializeArguments();

            _sliderMasterVolume.onValueChanged.AddListener(
                value => OnMasterChanged?.Invoke(value));
            _sliderAmbientVolume.onValueChanged.AddListener(
                value => OnAmbientChanged?.Invoke(value));
            _sliderEffectVolume.onValueChanged.AddListener(
                value => OnEffectChanged?.Invoke(value));
            _muteButton.OnClick.AddListener(() => 
            {
                OnMuteClicked?.Invoke();
                _muteButton.ToggleMuteState();
            });
        }

        public void SetInitialValues(float master, float ambient, float effect, bool isMute)
        {
            ValidateVolumeValues(master, ambient, effect);
            _sliderMasterVolume.SetValueWithoutNotify(master);
            _sliderAmbientVolume.SetValueWithoutNotify(ambient);
            _sliderEffectVolume.SetValueWithoutNotify(effect);
            _muteButton.SetMuteState(isMute);
        }

        private void ValidateInitializeArguments()
        {
            if (_sliderMasterVolume == null)
            {
                throw new NullReferenceException(
                    "Master volume slider reference is missing in VolumeSliderViewHandler.");
            }

            if (_sliderAmbientVolume == null)
            {
                throw new NullReferenceException(
                    "Ambient volume slider reference is missing in VolumeSliderViewHandler.");
            }

            if (_sliderEffectVolume == null)
            {
                throw new NullReferenceException(
                    "Effect volume slider reference is missing in VolumeSliderViewHandler.");
            }

            if (_muteButton == null)
            {
                throw new NullReferenceException(
                    "Mute button reference is missing in VolumeSliderViewHandler.");
            }
        }

        private void ValidateVolumeValues(float master, float ambient, float effect)
        {
            int maxVolume = 1;
            int minVolume = 0;

            if (master < minVolume || master > maxVolume)
            {
                throw new ArgumentOutOfRangeException(nameof(master),
                    "Master volume value must be between 0 and 1.");
            }

            if (ambient < minVolume || ambient > maxVolume)
            {
                throw new ArgumentOutOfRangeException(nameof(ambient),
                    "Ambient volume value must be between 0 and 1.");
            }

            if (effect < minVolume || effect > maxVolume)
            {
                throw new ArgumentOutOfRangeException(nameof(effect),
                    "Effect volume value must be between 0 and 1.");
            }
        }
    }
}