using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private readonly int _beginIndex = 0;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _interactClips;

    public void PlayInteractSound()
    {
        if (_interactClips == null || _interactClips.Length == 0)
            return;

        int randomIndex = Random.Range(_beginIndex, _interactClips.Length);
        AudioClip clipToPlay = _interactClips[randomIndex];

        _audioSource.PlayOneShot(clipToPlay);
    }
}