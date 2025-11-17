using FactoryCore;
using Language;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace EntryPoint
{
    public class EntryPointMainMenu : MonoBehaviour
    {
        [SerializeField] private PlatformDependentMenuSetter _platformDependentMenuSetter;
        [SerializeField] private StoreItemFactory _storeItemFactory;
        [SerializeField] private MonoBehaviour[] _servicesMono;

        private LanguageSetter _languageSetter;
        private List<IObjectInitilizable> _servicesInitializable = new();

        private void Awake()
        {
            _languageSetter = new LanguageSetter(YG2.lang);

            _platformDependentMenuSetter.Initialize(_languageSetter);

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
                currentObject.Initilize();
            }

            yield return new WaitUntil(()
                => _servicesInitializable.TrueForAll(currentObject => currentObject.IsInitialized));

            _storeItemFactory.Spawn();
        }
    }
}