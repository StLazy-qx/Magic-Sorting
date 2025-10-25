using System.Collections;
using UnityEngine;
using YG;

public class EntryPointGameSession : MonoBehaviour
{
    [Header("UI Canvas")]
    [SerializeField] private CanvasMobileSetter _mobileCanvas;
    [SerializeField] private CanvasDesktopSetter _desktopCanvas;
    [SerializeField] private DesktopObjectsStartPositioner _desktopObjectsPosition;
    [SerializeField] private MobileObjectsStartPositioner _mobileObjectsPosition;
    [Header("Game Systems")]
    [SerializeField] private Player _player;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private ColumnsFactory _columnsFactory;
    [SerializeField] private VesselFactory _vesselFactory;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private VesselsFullingBehaviour _vesselsFulling;
    [SerializeField] private FinalGameSession _finalGameSession;

    //[SerializeField] private LanguageSetter _languageSetter; // настроить определение языка на сцене
    //[SerializeField] protected SoundSetter;

    [SerializeField] private IObjectInitilizable[] _objectsInitilizable;

    private bool IsInitialization = false;

    public string OperatingSystem => YG2.envir.deviceType;

    private void Awake()
    {
        _mobileCanvas.Disable();
        _desktopCanvas.Disable();

        if (YG2.envir.isMobile)
        {
            _mobileCanvas.Enable();
            _mobileObjectsPosition.Initialize();
            _vesselsFulling.UseMobilePanel();
            _finalGameSession.UseMobilePanel();
        }
        else
        {
            _desktopCanvas.Enable();
            _desktopObjectsPosition.Initialize();
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
        _playerInventory.Initilize();

        yield return new WaitUntil(() => _vesselFactory.IsReady);

        if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
        {
            _columnsFactory.Initialize(_vesselFactory.Objects);
        }
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
