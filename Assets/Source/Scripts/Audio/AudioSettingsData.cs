using System;
using UnityEngine;

namespace Sound
{
    public class AudioSettingsData
    {
        private const float MaxVolume = 1f;

        public float MasterVolume { get; private set; } = MaxVolume;
        public float AmbientVolume { get; private set; } = MaxVolume;
        public float EffectVolume { get; private set; } = MaxVolume;
        public bool IsMuted { get; private set; }

        public void SetMasterVolume(float value)
        {
            MasterVolume = ValidateVolumeValue(value);
        }

        public void SetAmbientVolume(float value)
        {
            AmbientVolume = ValidateVolumeValue(value);
        }

        public void SetEffectVolume(float value)
        {
            EffectVolume = ValidateVolumeValue(value);
        }

        public void SetMute(bool value)
            => IsMuted = value;

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