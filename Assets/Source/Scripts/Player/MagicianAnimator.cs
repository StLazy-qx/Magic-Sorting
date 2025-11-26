using System;
using UnityEngine;

namespace Assets.Source.Scripts.Player
{
    [RequireComponent(typeof(Animator))]

    public class MagicianAnimator : MonoBehaviour
    {
        private readonly int AnimationInteract = Animator.StringToHash("Interact");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
                throw new ArgumentNullException(nameof(_animator));
        }

        public void PlayInteract()
            => _animator.SetTrigger(AnimationInteract);
    }
}