using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Assets.Source.Scripts.MagicCells
{
    public class MagicCellMover : MonoBehaviour
    {
        private const int InfinityAnimation = -1;
        private const float BeginTimeToShake = 0.8f;
        private const float LostTimeToShake = 1.5f;

        [Header("Idle Move")]
        [SerializeField] private float _moveDistance;
        [SerializeField] private float _durationMove;
        [Header("Shake")]
        [SerializeField] private float _shakeStrength;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private int _shakeVibrato;
        [Header("Arc Move")]
        [SerializeField] private float _arcMoveDuration = 1f;
        [SerializeField] private float _sideOffsetX = 1f;

        private float _secondToShake;
        private Tween _moveTween;

        private void Awake()
        {
            ValidateObjects();

            _secondToShake = Random.Range(BeginTimeToShake, LostTimeToShake);
        }

        private void Start()
        {
            _moveTween = transform.DOMoveY(
                transform.position.y - _moveDistance, _durationMove).
                SetEase(Ease.InOutSine).
                SetLoops(InfinityAnimation, LoopType.Yoyo).
                SetLink(gameObject);

            StartCoroutine(ShakeLoop());
        }

        private void OnDestroy()
        {
            _moveTween?.Kill();
        }

        public void MoveArc(Vector3 targetPosition)
        {
            _moveTween?.Kill();

            Vector3 start = transform.position;

            Vector3 mid = (start + targetPosition) * 0.5f;
            mid.x += _sideOffsetX;

            Vector3[] path =
            {
                start,
                mid,
                targetPosition
            };

            _moveTween = transform
                .DOPath(path, _arcMoveDuration, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .OnComplete(() =>
                {
                    if (transform != null)
                        transform.position = targetPosition;

                    _moveTween = null;
                });
        }

        private IEnumerator ShakeLoop()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(_secondToShake);

                transform.DOShakePosition(
                    _shakeDuration,
                    _shakeStrength,
                    _shakeVibrato).SetLink(gameObject);
            }
        }

        private void ValidateObjects()
        {
            if (_moveDistance <= 0)
            {
                throw new System.ArgumentException(
                    "Move distance must be positive", nameof(_moveDistance));
            }

            if (_durationMove <= 0)
            {
                throw new System.ArgumentException(
                    "Move duration must be positive", nameof(_durationMove));
            }
        }
    }
}