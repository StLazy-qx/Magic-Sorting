using System;
using UnityEngine;

namespace Sound
{
    public class AudioSettingsData
    {
        private const float MaxVolume = 1f;

        public float Master { get; private set; }
        public float Ambient { get; private set; }
        public float Effect { get; private set; }
        public bool IsMuted { get; private set; }

        public AudioSettingsData()
        {
            IsMuted = false;
            Master = MaxVolume;
            Ambient = MaxVolume;
            Effect = MaxVolume;
        }

        public void SetMasterVolume(float value)
        {
            Master = ValidateVolumeValue(value);
        }

        public void SetAmbientVolume(float value)
        {
            Ambient = ValidateVolumeValue(value);
        }

        public void SetEffectVolume(float value)
        {
            Effect = ValidateVolumeValue(value);
        }

        public void ChangeMuteState()
            => IsMuted = !IsMuted;

        private float ValidateVolumeValue(float value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value), "Volume value cannot be negative");
            }

            return Mathf.Clamp01(value);
        }
    }
}