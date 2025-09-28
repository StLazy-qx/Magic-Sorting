using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _spinSpeed;

    private void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), 360f / _spinSpeed, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental)
            .SetLink(gameObject);
    }
}
