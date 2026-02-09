using UnityEngine;

namespace Assets.Source.Scripts.ActionsHandlers
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxAngle;

        private float _currentAngle;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float input = 0f;

            if (Input.GetKey(KeyCode.A)) 
                input = 1f;

            if (Input.GetKey(KeyCode.D)) 
                input = -1f;

            float deltaAngle = input * _speed * Time.deltaTime;
            float newAngle = Mathf.Clamp(_currentAngle + deltaAngle, -_maxAngle, _maxAngle);
            float angleToRotate = newAngle - _currentAngle;

            transform.RotateAround(
                _target.position,
                Vector3.up,
                angleToRotate
            );

            _currentAngle = newAngle;
        }
    }
}
