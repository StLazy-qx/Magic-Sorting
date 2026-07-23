using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Scripts.EntryPoint
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private PlatformMenuAdapter _platformSetter;
        [SerializeField] private StoreItemFactory _storeItemFactory;
        [SerializeField] private MonoBehaviour[] _servicesMono;

        private LanguageSetter _languageSetter;
        private List<IObjectInitilizable> _servicesInitializable = new();

        private void Awake()
        {
            ValidateDependencies();

            _languageSetter = new LanguageSetter();

            _platformSetter.Initialize(_languageSetter);

            foreach (var mono in _servicesMono)
            {
                if (mono is IObjectInitilizable initObj)
                    _servicesInitializable.Add(initObj);
            }

            StartCoroutine(InitializeServices());
        }

        private IEnumerator InitializeServices()
        {
            _storeItemFactory.Spawn();

            foreach (IObjectInitilizable currentObject in _servicesInitializable)
                currentObject.Initialize();

            yield return new WaitUntil(()
                => _servicesInitializable.TrueForAll
                (currentObject => currentObject.IsInitialized));
        }

        private void ValidateDependencies()
        {
            if (_platformSetter == null)
                throw new ArgumentNullException(nameof(_platformSetter));

            if (_storeItemFactory == null)
                throw new ArgumentNullException(nameof(_storeItemFactory));

            if (_servicesMono == null)
                throw new ArgumentNullException(nameof(_servicesMono));
        }
    }
}