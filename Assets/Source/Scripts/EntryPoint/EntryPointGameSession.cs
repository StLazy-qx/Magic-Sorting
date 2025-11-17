using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FactoryCore;

namespace EntryPoint
{
    public class EntryPointGameSession : MonoBehaviour
    {
        [SerializeField] private PlatformDependentSetter _platformDependentSetter;
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private StoreItemFactory _storeItemFactory;
        [SerializeField] private MonoBehaviour[] _objectsToInitializeMono;

        private List<IObjectInitilizable> _objectsInitilizable = new();

        private void Awake()
        {
            _platformDependentSetter.Initialize();

            foreach (var mono in _objectsToInitializeMono)
            {
                if (mono is IObjectInitilizable initObj)
                    _objectsInitilizable.Add(initObj);
            }

            StartCoroutine(SessionInitialize());
        }

        private IEnumerator SessionInitialize()
        {
            yield return StartCoroutine(FactoryInitialize());
            yield return StartCoroutine(EntityInitialize());
        }

        private IEnumerator FactoryInitialize()
        {
            _vesselFactory.Spawn();
            _storeItemFactory.Spawn();

            yield return new WaitUntil(() => _vesselFactory.IsReady);

            if (_vesselFactory.Objects != null && _vesselFactory.Objects.Count > 0)
            {
                _columnsFactory.Initialize(_vesselFactory.Objects);
                _columnsFactory.Spawn();
            }
        }

        private IEnumerator EntityInitialize()
        {
            foreach (IObjectInitilizable currentObject in _objectsInitilizable)
            {
                currentObject.Initilize();
            }

            yield return new WaitUntil(()
                => _objectsInitilizable.TrueForAll(currentObject => currentObject.IsInitialized));
        }
    }
}