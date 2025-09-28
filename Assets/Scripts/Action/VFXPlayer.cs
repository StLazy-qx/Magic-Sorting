using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _interactEffect;
    //[SerializeField] private Transform _magicEffectPoint;

    //private void Awake()
    //{
    //    if (_interactEffect != null)
    //    {
    //        _interactEffect.transform.position = transform.position;
    //        _interactEffect.transform.parent = transform;
    //    }
    //}

    public void InteractEffect()
    {
        _interactEffect.Play();

        //if (_interactEffect == null)
        //    return;

        //_interactEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        //if (!_interactEffect.isPlaying)
        //{
        //    _interactEffect.Play();
        //}
    }
}