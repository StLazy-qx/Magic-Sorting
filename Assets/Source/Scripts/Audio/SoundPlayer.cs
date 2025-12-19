using System;
using System.Collections;
using UnityEngine;

namespace Assets.Source.Scripts.Audio
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _interactClips;

        private bool _isPlaying;

        public void PlayInteractSound()
        {
            if (_audioSource == null)
            {
                throw new ArgumentNullException
                    (nameof(_audioSource), "AudioSource cannot be null");
            }

            if (_interactClips.Length == 0 || _interactClips == null)
                return;

            if (_isPlaying)
                return;

            int beginIndex = 0;
            int randomIndex = UnityEngine.Random.Range(beginIndex, _interactClips.Length);
            AudioClip clip = _interactClips[randomIndex];

            StartCoroutine(PlaySoundRoutine(clip));
        }

        private IEnumerator PlaySoundRoutine(AudioClip clip)
        {
            _isPlaying = true;

            _audioSource.PlayOneShot(clip);

            yield return new WaitForSeconds(clip.length);

            _isPlaying = false;
        }
    }
}