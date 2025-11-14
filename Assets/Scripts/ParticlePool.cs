using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private Transform _container;

    private Queue<ParticleSystem> _particles = new();

    public void Initialize(int count)
    {
        if (count <= 0)
            return;

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
}
