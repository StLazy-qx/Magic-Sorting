using Assets.Source.Scripts.Audio;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Player;
using Assets.Source.Scripts.Pool;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.Source.Scripts.ActionsHandlers
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

        private void OnPlayerAction(
            Vector3 beginPosition, 
            Vector3 targetPosition, 
            Color color)
        {
            ValidateParameters(beginPosition, targetPosition);
            _animator.PlayInteract();
            _soundPlayer.PlayInteractSound();
            StartCoroutine(MoveParticle(
                beginPosition, 
                targetPosition, 
                color));
        }

        private IEnumerator MoveParticle(
            Vector3 beginPosition, 
            Vector3 targetPosition, 
            Color color)
        {
            ParticleSystem particle = _particlePool.HandOver();

            if (particle == null)
                yield break;

            SetupParticle(particle, beginPosition, color);

            yield return AnimateParticleMovement(particle, beginPosition, targetPosition);

            particle.Stop();
        }

        private void SetupParticle(
            ParticleSystem particle, 
            Vector3 beginPosition, 
            Color color)
        {
            particle.transform.position = beginPosition;
            particle.transform.rotation = Quaternion.identity;

            SetParticleColor(particle, color);
            particle.Play();
        }

        private void SetParticleColor(ParticleSystem particle, Color color)
        {
            ParticleSystem.MainModule main = particle.main;
            main.startColor = color;

            ParticleSystem[] childParticles = 
                particle.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem childParticle in childParticles)
            {
                if (childParticle != particle)
                {
                    ParticleSystem.MainModule childMain = childParticle.main;
                    childMain.startColor = color;
                }
            }
        }

        private IEnumerator AnimateParticleMovement(
            ParticleSystem particle, 
            Vector3 beginPosition, 
            Vector3 targetPosition)
        {
            float elapsed = 0f;

            while (elapsed < _moveDuration)
            {
                elapsed += Time.deltaTime;
                float currentTime = Mathf.Clamp01(elapsed / _moveDuration);
                particle.transform.position = Vector3.Lerp
                    (beginPosition, targetPosition, currentTime);

                yield return null;
            }
        }

        private void ValidateParameters(Vector3 beginPosition, Vector3 targetPosition)
        {
            if (_animator == null)
            {
                throw new ArgumentNullException
                    (nameof(_animator), "MagicianAnimator cannot be null");
            }

            if (_soundPlayer == null)
            {
                throw new ArgumentNullException
                    (nameof(_soundPlayer), "SoundPlayer cannot be null");
            }

            if (_cellRouter == null)
            {
                throw new ArgumentNullException
                    (nameof(_cellRouter), "MagicCellRouter cannot be null");
            }

            if (_particlePool == null)
            {
                throw new ArgumentNullException
                    (nameof(_particlePool), "ParticlePool cannot be null");
            }

            if (_moveDuration <= 0)
            {
                throw new InvalidOperationException
                    ("Move duration must be positive.");
            }

            if (beginPosition.IsValid() == false ||
                targetPosition.IsValid() == false)
            {
                throw new ArgumentException("Position contains NaN or Infinity");
            }
        }
    }
}