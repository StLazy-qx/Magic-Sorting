using UnityEngine;

public class VesselDisabledEffector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void PlayEffect(Vector3 vesselPosition)
    {
        _particle.transform.position = vesselPosition;
        _particle.Play();
    }
}
