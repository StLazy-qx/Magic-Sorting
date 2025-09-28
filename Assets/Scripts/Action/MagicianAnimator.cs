using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class MagicianAnimator : MonoBehaviour
{
    private readonly int _animationInteract = Animator.StringToHash("Interact");

    private Animator _animator;

    public event Action InteractAnimating;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayInteract()
    {
        _animator.SetTrigger(_animationInteract);
    }
}
