using Assets.Source.Scripts.Pool;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.Source.Scripts.Vessels
{
    public class VesselCompletionEffecter : MonoBehaviour, IEffectPoolInitializable
    {
        private readonly float _offsetY = 0.3f;

        [SerializeField] private ParticlePool _particlePool;

        public event Action<int> PoolEffectSizeReading;

        public void Initialize(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Particle count must be greater than zero.");
            }

            PoolEffectSizeReading?.Invoke(value);
        }

        public IEnumerator PlayEffect(
            Vector3 position,
            Color color,
            float duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(duration),
                    "Effect duration must be greater than zero.");
            }

            ParticleSystem particle = _particlePool.HandOver();

            if (particle == null)
            {
                throw new InvalidOperationException(
                    "No available particles in pool.");
            }

            particle.transform.position = new Vector3
                (position.x, position.y - _offsetY, position.z);
            var main = particle.main;
            main.startColor = color;

            particle.Play();

            yield return new WaitForSeconds(duration);
        }
    }
}