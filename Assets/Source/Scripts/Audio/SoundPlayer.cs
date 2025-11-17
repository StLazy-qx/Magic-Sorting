using System;
using UnityEngine;

namespace Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _interactClips;

        public void PlayInteractSound()
        {
            if (_audioSource == null)
            {
                throw new ArgumentNullException
                    (nameof(_audioSource), "AudioSource cannot be null");
            }

            if (_interactClips.Length == 0)
                return;

            int beginIndex = 0;
            int randomIndex = UnityEngine.Random.Range(beginIndex, _interactClips.Length);
            AudioClip clipToPlay = _interactClips[randomIndex];

            _audioSource.PlayOneShot(clipToPlay);
        }
    }
}