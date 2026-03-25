using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Assets.Source.Scripts.EntryPoint
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private PlatformMenuAdapter _platformSetter;
        [SerializeField] private StoreItemFactory _storeItemFactory;
        //[SerializeField] private LoadingWindow _loadingWindowPrefab;
        [SerializeField] private MonoBehaviour[] _servicesMono;

        private LanguageSetter _languageSetter;
        //private LoadingWindow _loadingWindow;
        private List<IObjectInitilizable> _servicesInitializable = new();

        private void Awake()
        {
            ValidateDependencies();

            //_loadingWindow = Instantiate(_loadingWindowPrefab);
            _languageSetter = new LanguageSetter(YG2.lang);

            _platformSetter.Initialize(/*_loadingWindow,*/ _languageSetter);

            foreach (var mono in _servicesMono)
            {
                if (mono is IObjectInitilizable initObj)
                    _servicesInitializable.Add(initObj);
            }

            StartCoroutine(InitializeServices());
        }

        private IEnumerator InitializeServices()
        {
            foreach (IObjectInitilizable currentObject in _servicesInitializable)
            {
                currentObject.Initialize();
            }

            yield return new WaitUntil(()
                => _servicesInitializable.TrueForAll
                (currentObject => currentObject.IsInitialized));

            _storeItemFactory.Spawn();
        }

        private void ValidateDependencies()
        {
            //if (_loadingWindowPrefab == null)
            //    throw new ArgumentNullException(nameof(_loadingWindowPrefab));

            if (_platformSetter == null)
                throw new ArgumentNullException(nameof(_platformSetter));

            if (_storeItemFactory == null)
                throw new ArgumentNullException(nameof(_storeItemFactory));

            if (_servicesMono == null)
                throw new ArgumentNullException(nameof(_servicesMono));
        }
    }
}