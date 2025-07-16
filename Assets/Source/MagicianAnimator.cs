using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MagicianAnimator : MonoBehaviour
{
    private readonly int _animationInteract = Animator.StringToHash("Interact");

    [SerializeField] private MagicColumn[] _magicColumns;

    private Animator _animator;

    public event Action InteractAnimating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        foreach (MagicColumn column in _magicColumns)
        {
            if (column != null)
            {
                column.CellDisplacing += OnInteractAnimated;
            }
        }
    }

    private void OnDisable()
    {
        foreach (MagicColumn column in _magicColumns)
        {
            if (column != null)
            {
                column.CellDisplacing -= OnInteractAnimated;
            }
        }
    }

    private void OnInteractAnimated()
    {
        _animator.SetTrigger(_animationInteract);
    }
}
