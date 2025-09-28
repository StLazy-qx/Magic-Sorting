using System.Collections;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    [SerializeField] private MagicianAnimator _animator;
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private MagicCellRouter _cellRouter;
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
        ParticleSystem particle = Instantiate(
            _particlePrefab, 
            beginPosition, 
            Quaternion.identity);

        ParticleSystem.MainModule main = particle.main;
        main.startColor = color;

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

