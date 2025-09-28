using DG.Tweening;
using UnityEngine;

public class CellMover : MonoBehaviour
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _durationMove;
    [SerializeField] private float _shakeStrength = 0.1f;
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private int _shakeVibrato = 10;

    

    private void Start()
    {
        //transform.DOMoveY(transform.position.y - _moveDistance, _durationMove)
        //    .SetEase(Ease.InOutSine)
        //    .SetLoops(-1, LoopType.Yoyo)
        //    .SetLink(gameObject);

        Sequence _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOMoveY(transform.position.y - _moveDistance, _durationMove)
                .SetEase(Ease.InOutSine));

        _sequence.Join(transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato));

        _sequence.SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }
}
