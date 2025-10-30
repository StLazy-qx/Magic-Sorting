using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class EntryPointGameSession : MonoBehaviour
{
    [SerializeField] private CanvasMobileSetter _mobileCanvas;
    [SerializeField] private CanvasDesktopSetter _desktopCanvas;
    [SerializeField] private ObjectsBeginPositionSetter _desktopObjectsPosition;
    [SerializeField] private ObjectsBeginPositionSetter _mobileObjectsPosition;
    [SerializeField] private Player _player;
    [SerializeField] private ColumnsFactory _columnsFactory;
    [SerializeField] private VesselFactory _vesselFactory;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private VesselStateTracker  _vesselsFulling;
    [SerializeField] private FinalGameSession _finalGameSession;

    //[SerializeField] private LanguageSetter _languageSetter; // настроить определение языка на сцене
    //[SerializeField] protected SoundSetter;

    [SerializeField] private MonoBehaviour[] _objectsToInitializeMono;

    private List<IObjectInitilizable> _objectsInitilizable = new();

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

        foreach (var mono in _objectsToInitializeMono)
        {
            if (mono is IObjectInitilizable initObj)
                _objectsInitilizable.Add(initObj);
        }
    }

    private void Start()
    {
        StartCoroutine(SessionInitialize());
    }

    private IEnumerator SessionInitialize()
    {
        yield return StartCoroutine(FactoryInitialize());
        yield return StartCoroutine(EntityInitialize());

        Debug.Log("<color=green>Все объекты успешно инициализированы!</color>");
    }

    private IEnumerator FactoryInitialize()
    {
        yield return new WaitUntil(() => _vesselFactory.IsReady);

        if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
        {
            _columnsFactory.Initialize(_vesselFactory.Objects);
        }
    }

    private IEnumerator EntityInitialize()
    {
        foreach (var currentObject in _objectsInitilizable)
        {
            currentObject.Initilize();
        }

        yield return new WaitUntil(() 
            => _objectsInitilizable.TrueForAll(currentObject => currentObject.IsInitialized));
    }
}
