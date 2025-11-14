using UnityEngine;

namespace PlayerCore
{
    [RequireComponent(typeof(Animator))]

    public class MagicianAnimator : MonoBehaviour
    {
        private readonly int _animationInteract = Animator.StringToHash("Interact");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayInteract()
        {
            if (_animator == null)
                return;

            _animator.SetTrigger(_animationInteract);
        }

    }
}