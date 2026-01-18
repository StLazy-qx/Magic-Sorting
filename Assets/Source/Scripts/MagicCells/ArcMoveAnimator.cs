using UnityEngine;
using DG.Tweening;

namespace Assets.Source.Scripts.MagicCells
{
    class ArcMoveAnimator : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _sideOffsetX = 1f;

        private Tween _moveTween;

        [ContextMenu("TestMove")]
        public void TestMove()
        {
            Move(transform.position + Vector3.up * 3f);
        }

        public void Move(Vector3 targetPosition)
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

            _moveTween = transform.DOPath(
                    path,
                    _duration,
                    PathType.CatmullRom
                )
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (transform != null)
                        transform.position = targetPosition;

                    _moveTween = null;
                });
        }
    }
}
