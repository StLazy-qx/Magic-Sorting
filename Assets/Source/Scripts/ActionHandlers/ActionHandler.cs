using System.Collections;
using UnityEngine;
using Sound;
using MagicCells;
using PlayerCore;
using Pool;

namespace ActionHandler
{
    public class ActionHandler : MonoBehaviour
    {
        [SerializeField] private MagicianAnimator _animator;
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private ParticlePool _particlePool;
        [SerializeField] private float _moveDuration = 1f;

        private void OnEnable()
        {
            _cellRouter.CellDelivering += OnPlayerAction;
        }

        private void OnDisable()
        {
            _cellRouter.CellDelivering -= OnPlayerAction;
        }

        private void OnPlayerAction(Vector3 beginPosition, Vector3 targetPosition, Color color)
        {
            _animator.PlayInteract();
            _soundPlayer.PlayInteractSound();

            StartCoroutine(MoveParticle(beginPosition, targetPosition, color));
        }

        private IEnumerator MoveParticle(Vector3 beginPosition, Vector3 targetPosition, Color color)
        {
            ParticleSystem particle = _particlePool.HandOver();

            if (particle == null)
                yield break;

            particle.transform.position = beginPosition;
            particle.transform.rotation = Quaternion.identity;

            ParticleSystem.MainModule main = particle.main;
            main.startColor = color;

            ParticleSystem[] childParticles = particle.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem childParticle in childParticles)
            {
                if (childParticle != particle)
                {
                    ParticleSystem.MainModule childMain = childParticle.main;
                    childMain.startColor = color;
                }
            }

            particle.Play();

            float elapsed = 0f;

            while (elapsed < _moveDuration)
            {
                elapsed += Time.deltaTime;
                float currentTime = Mathf.Clamp01(elapsed / _moveDuration);

                particle.transform.position = Vector3.Lerp(beginPosition, targetPosition, currentTime);

                yield return null;
            }

            particle.Stop();
        }
    }
}

