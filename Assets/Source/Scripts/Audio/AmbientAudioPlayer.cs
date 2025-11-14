using System.Collections;
using UnityEngine;
using System.Linq;

public class AmbientAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;

    private AudioClip[] _shuffledClips;
    private int _currentIndex;

    private void Start()
    {
        if (_clips == null
            || _clips.Length == 0
            || _source == null)
        {
            return;
        }

        ShuffleClips();
        StartCoroutine(PlayClipsInSequence());
    }

    private void ShuffleClips()
    {
        _shuffledClips = _clips.OrderBy(clip => Random.value).ToArray();
        _currentIndex = 0;
    }

    private IEnumerator PlayClipsInSequence()
    {
        while (isActiveAndEnabled)
        {
            if (_currentIndex >= _shuffledClips.Length)
                ShuffleClips();

            AudioClip currentClip = _shuffledClips[_currentIndex];
            _source.clip = currentClip;

            _source.Play();

            yield return new WaitForSeconds(currentClip.length);

            _currentIndex++;
        }
    }
}
