using System.Collections;
using UnityEngine;
using Pool;

namespace Vessels
{
    public class VesselCompletionEffecter : MonoBehaviour
    {
        private readonly float _offsetY = 0.3f;

        [SerializeField] private ParticlePool _particlePool;

        public void Initialize(int value)
        {
            _particlePool.Initialize(value);
        }

        public IEnumerator PlayEffect(
            Vector3 position,
            Color color,
            float duration)
        {
            ParticleSystem particle = _particlePool.HandOver();
            particle.transform.position = new Vector3
                (position.x, position.y - _offsetY, position.z);
            var main = particle.main;
            main.startColor = color;

            particle.Play();

            yield return new WaitForSeconds(duration);
        }
    }
}