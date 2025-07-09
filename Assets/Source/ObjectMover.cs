using DG.Tweening;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _durationMove;

    private void Start()
    {
        transform.DOMoveY(transform.position.y - _moveDistance, _durationMove)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}
