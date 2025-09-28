using UnityEngine;

public class AudioSettingsData
{
    private const float MaxVolume = 1f;

    public float MasterVolume { get; private set; } = MaxVolume;
    public float AmbientVolume { get; private set; } = MaxVolume;
    public float EffectVolume { get; private set; } = MaxVolume;
    public bool IsMuted { get; private set; }

    public void SetMasterVolume(float value)
        => MasterVolume = Mathf.Clamp01(value);

    public void SetAmbientVolume(float value)
    => AmbientVolume = Mathf.Clamp01(value);

    public void SetEffectVolume(float value)
    => EffectVolume = Mathf.Clamp01(value);

    public void SetMute(bool value)
        => IsMuted = value;
}
