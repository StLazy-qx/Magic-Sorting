using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.Scripts.Factory;
using System;

namespace Assets.Source.Scripts.EntryPoint
{
    public class EntryPointGameSession : MonoBehaviour
    {
        [SerializeField] private PlatformGameAdapter _platformDependentSetter;
        [SerializeField] private ColumnsFactory _columnsFactory;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private StoreItemFactory _storeItemFactory;
        [SerializeField] private MonoBehaviour[] _objectsToInitializeMono;

        private List<IObjectInitilizable> _objectsInitilizable = new();

        private void Awake()
        {
            ValidateDependencies();
            _platformDependentSetter.Initialize();
            CollectInitializableObjects();
            StartCoroutine(SessionInitialize());
        }

        private void CollectInitializableObjects()
        {
            if (_objectsToInitializeMono.Length == 0)
                return;

            foreach (var mono in _objectsToInitializeMono)
            {
                if (mono is null)
                {
                    throw new ArgumentNullException(nameof(mono),
                        "Element inside _objectsToInitializeMono is null.");
                }

                if (mono is IObjectInitilizable initObj)
                    _objectsInitilizable.Add(initObj);
            }
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

            if (_vesselFactory.Objects == null)
                throw new InvalidOperationException("VesselFactory.Objects is null.");

            if (_vesselFactory.Objects.Count > 0)
            {
                _columnsFactory.Initialize(_vesselFactory.Objects);
                _columnsFactory.Spawn();
            }
        }

        private IEnumerator EntityInitialize()
        {
            if (_objectsInitilizable.Count == 0)
                yield break;

            foreach (IObjectInitilizable currentObject in _objectsInitilizable)
            {
                currentObject.Initialize();
            }

            yield return new WaitUntil(()
                => _objectsInitilizable.TrueForAll(currentObject => currentObject.IsInitialized));
        }

        private void ValidateDependencies()
        {
            if (_platformDependentSetter == null)
                throw new ArgumentNullException(nameof(_platformDependentSetter));

            if (_columnsFactory == null)
                throw new ArgumentNullException(nameof(_columnsFactory));

            if (_vesselFactory == null)
                throw new ArgumentNullException(nameof(_vesselFactory));

            if (_storeItemFactory == null)
                throw new ArgumentNullException(nameof(_storeItemFactory));

            if (_objectsToInitializeMono == null)
                throw new ArgumentNullException(nameof(_objectsToInitializeMono));
        }
    }
}