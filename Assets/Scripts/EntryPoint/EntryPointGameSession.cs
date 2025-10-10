using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class EntryPointGameSession : MonoBehaviour
{
    [SerializeField] private CanvasMobileSetter _mobileCanvas;
    [SerializeField] private CanvasPCSetter _canvasPC;
    //[SerializeField] private LanguageSetter _languageSetter; // настроить определение языка на сцене
    //[SerializeField] protected DifficultyDatabase DifficultyDatabase;
    //[SerializeField] protected SoundSetter;

    //[SerializeField] private IObjectInitilizable[] _objectsInitilizable;

    [SerializeField] private Player _player;
    [SerializeField] private ColumnsFactory _columnsFactory;
    [SerializeField] private VesselFactory _vesselFactory;
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private VesselsFullingBehaviour _vesselsFulling;

    private bool IsInitialization = false;
    private IReadOnlyList<Vessel> _vessels;

    private void Awake()
    {
        _mobileCanvas.Disable();
        _canvasPC.Disable();

        if (YG2.envir.isMobile)
        {
            _mobileCanvas.Enable();
        }
        else
        {
            _canvasPC.Enable();
        }
    }

    private void Start()
    {
        StartCoroutine(VesselFactoryInitialize());
    }

    private void Init()
    {
        _columnsFactory.Initialize(_vessels);
        _vesselsFulling.Initialize(_vessels);
    }

    private IEnumerator VesselFactoryInitialize()
    {
        //кешировать
        yield return new WaitUntil(() => _vesselFactory.IsReady);

        if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
        {
            _vessels = _vesselFactory.Objects;

            Init();
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
