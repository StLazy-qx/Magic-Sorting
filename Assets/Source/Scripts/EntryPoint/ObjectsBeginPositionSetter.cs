using UnityEngine;

public class ObjectsBeginPositionSetter : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _chair;
    [SerializeField] private Transform _columnsPoints;

    [Header("Positions & Rotations")]
    [SerializeField] private Vector3 _cameraPosition;
    [SerializeField] private Vector3 _cameraRotation;
    [SerializeField] private Vector3 _playerPosition;
    [SerializeField] private Vector3 _playerRotation;
    [SerializeField] private Vector3 _chairPosition;
    [SerializeField] private Vector3 _columnsPointsPosition;

    public void Initialize()
    {
        if (ValidateObjects())
            PositionObjects();
    }

    private bool ValidateObjects()
    {
        if (_mainCamera == null)
            return false;

        if (_player == null)
            return false;

        if (_chair == null)
            return false;

        if (_columnsPoints == null)
            return false;

        return true;
    }

    private void PositionObjects()
    {
        _mainCamera.transform.SetPositionAndRotation(_cameraPosition, Quaternion.Euler(_cameraRotation));
        _player.transform.SetPositionAndRotation(_playerPosition, Quaternion.Euler(_playerRotation));
        _chair.transform.SetPositionAndRotation(_chairPosition, Quaternion.identity);
        _columnsPoints.transform.SetPositionAndRotation(_columnsPointsPosition, Quaternion.identity);
    }
}
