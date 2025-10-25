using UnityEngine;

public class MobileObjectsStartPositioner : MonoBehaviour
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
        {
            Debug.LogWarning("[DesctopObjectsStartPosition] Main Camera is not assigned in inspector!");

            return false;
        }

        if (_player == null)
        {
            Debug.LogWarning("[DesctopObjectsStartPosition] Player is not assigned in inspector!");

            return false;
        }

        if (_chair == null)
        {
            Debug.LogWarning("[DesctopObjectsStartPosition] Chair is not assigned in inspector!");

            return false;
        }

        if (_columnsPoints == null)
        {
            Debug.LogWarning("[DesctopObjectsStartPosition] Columns Points is not assigned in inspector!");

            return false;
        }

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
