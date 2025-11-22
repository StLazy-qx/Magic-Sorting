using UnityEngine.UI;
using UnityEngine;
using System;

namespace Sound
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
            _sliderMasterVolume.SetValueWithoutNotify(master);
            _sliderAmbientVolume.SetValueWithoutNotify(ambient);
            _sliderEffectVolume.SetValueWithoutNotify(effect);
            _muteButton.SetMuteState(isMute);
        }
    }
}