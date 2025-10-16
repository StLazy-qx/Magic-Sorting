using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _interactClips;

    public void PlayInteractSound()
    {
        int beginIndex = 0;

        if (_interactClips == null || _interactClips.Length == 0)
            return;

        int randomIndex = Random.Range(beginIndex, _interactClips.Length);
        AudioClip clipToPlay = _interactClips[randomIndex];

        _audioSource.PlayOneShot(clipToPlay);
    }
}