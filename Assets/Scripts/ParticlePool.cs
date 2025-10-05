using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlePrefab;

    private Queue<ParticleSystem> _particles = new();
    private Transform _container;

    public void Initialize(int count)
    {
        if (count <= 0)
            return;

        //как изменить код ниже

        _container = new GameObject("ParticlePool").transform;
        _container.SetParent(transform);

        for (int i = 0; i < count; i++)
        {
            ParticleSystem particle = Instantiate(_particlePrefab, _container);
            particle.gameObject.SetActive(false);
            _particles.Enqueue(particle);
        }
    }

    public ParticleSystem Get()
    {
        if (_particles.Count > 0)
        {
            ParticleSystem particle = _particles.Dequeue();

            particle.gameObject.SetActive(true);

            return particle;
        }

        return null;
    }
}
