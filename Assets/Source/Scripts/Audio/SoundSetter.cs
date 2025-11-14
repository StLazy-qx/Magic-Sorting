using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    public class SoundSetter
    {
        private const string MasterVolume = "Master";
        private const string AmbientVolume = "Ambient";
        private const string EffectVolume = "Effect";
        private const float MinVolume = 0.0001f;
        private const float MaxVolume = 1f;
        private const float VolumeMultiplier = 20f;
        private const float LogarithmMultiplier = 10f;
        private const float MuteDBValue = -80f;

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

            float dbValue = Mathf.Log10(Mathf.Clamp(volume, MinVolume, MaxVolume)) * VolumeMultiplier;
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
                return Mathf.Pow(LogarithmMultiplier, dbValue / VolumeMultiplier);

            return MaxVolume;
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
            _mixer.SetFloat(MasterVolume, MuteDBValue);
            _mixer.SetFloat(AmbientVolume, MuteDBValue);
            _mixer.SetFloat(EffectVolume, MuteDBValue);
        }

        private void RestoreVolumes()
        {
            SetVolume(MasterVolume, _settings.MasterVolume);
            SetVolume(AmbientVolume, _settings.AmbientVolume);
            SetVolume(EffectVolume, _settings.EffectVolume);
        }
    }
}