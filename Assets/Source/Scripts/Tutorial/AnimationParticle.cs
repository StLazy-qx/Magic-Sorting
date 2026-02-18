using System.Collections;
using UnityEngine;

namespace Assets.Source.Scripts.Tutorial
{
    class AnimationParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;

        private float _waitSecond = 3f;
        private Coroutine _playRoutine;
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_waitSecond);
        }

        public void Play(Vector3 position)
        {
            if (_particle == null)
                return;

            transform.position = position;

            gameObject.SetActive(true);

            if (_playRoutine != null)
                StopCoroutine(_playRoutine);

            _playRoutine = StartCoroutine(PlayRoutine());
        }

        private IEnumerator PlayRoutine()
        {
            _particle.Play();

            yield return _waitForSeconds;

            Stop();
        }

        public void Stop()
        {
            if (_particle == null)
                return;

            if (_particle.isPlaying)
                _particle.Stop();
        }
    }
}
