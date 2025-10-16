using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class EntryPointGameSession : MonoBehaviour
{
    [Header("UI Canvas")]
    [SerializeField] private CanvasMobileSetter _mobileCanvas;
    [SerializeField] private CanvasDesktopSetter _desktopCanvas;
    [Header("Main Camera")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Vector3 _mobileCameraPosition;
    [SerializeField] private Vector3 _mobileCameraRotation;
    [SerializeField] private Vector3 _pcCameraPosition;
    [SerializeField] private Vector3 _pcCameraRotation;
    [Header("Game Systems")]
    [SerializeField] private Player _player;
    [SerializeField] private ColumnsFactory _columnsFactory;
    [SerializeField] private VesselFactory _vesselFactory;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private VesselsFullingBehaviour _vesselsFulling;
    [SerializeField] private FinalGameSession _finalGameSession;

    //[SerializeField] private LanguageSetter _languageSetter; // настроить определение €зыка на сцене
    //[SerializeField] protected SoundSetter;

    //[SerializeField] private IObjectInitilizable[] _objectsInitilizable;

    //private bool IsInitialization = false;

    public string OperatingSystem => YG2.envir.deviceType;

    private void Awake()
    {
        _mobileCanvas.Disable();
        _desktopCanvas.Disable();

        if (YG2.envir.isMobile)
        {
            _mobileCanvas.Enable();
            SetupCamera(_mobileCameraPosition, _mobileCameraRotation);
            _vesselsFulling.UseMobilePanel();
            _finalGameSession.UseMobilePanel();
        }
        else
        {
            _desktopCanvas.Enable();
            SetupCamera(_pcCameraPosition, _pcCameraRotation);
            _vesselsFulling.UseDesctopPanel();
            _finalGameSession.UseDesctopPanel();
        }
    }

    private void Start()
    {
        StartCoroutine(FactoryInitialize());
    }

    private IEnumerator FactoryInitialize()
    {
        yield return new WaitUntil(() => _vesselFactory.IsReady);

        if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
        {
            _columnsFactory.Initialize(_vesselFactory.Objects);
        }
    }

    private void SetupCamera(Vector3 position, Vector3 rotation)
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("[EntryPointGameSession] " +
                " амера не назначена в инспекторе!");

            return;
        }

        _mainCamera.transform.position = position;
        _mainCamera.transform.rotation = Quaternion.Euler(rotation);
    }

    //private IEnumerator EntityInitilize()
    //{
    //    while (IsInitialization != true)
    //    {
    //        foreach (IObjectInitilizable @object in _objectsInitilizable)
    //        {
    //            @object.Initilize()
    //        }
    //    }
    //}
}
