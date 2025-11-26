using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.Pool
{
    public class ParticlePool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private Transform _container;

        private Queue<ParticleSystem> _particles = new();

        public void Initialize(int count)
        {
            ValidateArguments(count);
            _particles.Clear();
            _container.SetParent(transform);

            for (int i = 0; i < count; i++)
            {
                ParticleSystem particle = Instantiate(_particlePrefab, _container);

                particle.gameObject.SetActive(false);
                _particles.Enqueue(particle);
            }
        }

        public ParticleSystem HandOver()
        {
            if (_particles.TryDequeue(out ParticleSystem particle))
            {
                particle.gameObject.SetActive(true);

                return particle;
            }

            return null;
        }

        private void ValidateArguments(int count)
        {
            if (_particlePrefab == null)
            {
                throw new System.ArgumentNullException(nameof(_particlePrefab));
            }

            if (_container == null)
            {
                throw new System.ArgumentNullException(nameof(_container));
            }

            if (count <= 0)
            {
                throw new System.ArgumentException(
                    "Count must be greater than zero", nameof(count));
            }
        }
    }
}