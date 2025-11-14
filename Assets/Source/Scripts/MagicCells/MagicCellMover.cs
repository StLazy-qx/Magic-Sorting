using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace MagicCells
{
    public class MagicCellMover : MonoBehaviour
    {
        private const int InfinityAnimation = -1;
        private const float BeginTimeToShake = 0.8f;
        private const float LostTimeToShake = 1.5f;

        [SerializeField] private float _moveDistance;
        [SerializeField] private float _durationMove;
        [SerializeField] private float _shakeStrength = 0.1f;
        [SerializeField] private float _shakeDuration = 0.2f;
        [SerializeField] private int _shakeVibrato = 3;

        private float _secondToShake;
        private Tween _moveTween;

        private void Awake()
        {
            _secondToShake = Random.Range(BeginTimeToShake, LostTimeToShake);
        }

        private void Start()
        {
            _moveTween = transform.DOMoveY(transform.position.y - _moveDistance, _durationMove)
                .SetEase(Ease.InOutSine)
                .SetLoops(InfinityAnimation, LoopType.Yoyo)
                .SetLink(gameObject);

            StartCoroutine(ShakeLoop());
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
        }

        private IEnumerator ShakeLoop()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(_secondToShake);

                transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato)
                         .SetLink(gameObject);
            }
        }
    }
}