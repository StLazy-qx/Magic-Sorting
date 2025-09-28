using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _interactClip;

    public void PlayInteractSound()
    {
        if (_interactClip != null)
            _audioSource.PlayOneShot(_interactClip);
    }
}
