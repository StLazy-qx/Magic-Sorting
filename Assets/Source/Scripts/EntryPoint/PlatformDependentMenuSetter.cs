using FactoryCore;
using Language;
using UnityEngine;
using YG;

namespace EntryPoint
{
    public class PlatformDependentMenuSetter : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private CanvasMobileSetter _mobileCanvas;
        [SerializeField] private CanvasDesktopSetter _desktopCanvas;
        [Header("Panels")]
        [SerializeField] private LanguageView _languageViewDesktop;
        [SerializeField] private LanguageView _languageViewMobile;
        [Header("Store installation")]
        [SerializeField] private StoreItemFactory _itemFactory;
        [SerializeField] private Transform _desktopContent;
        [SerializeField] private Transform _mobileContent;

        public void Initilize(LanguageSetter languageSetter)
        {
            if (languageSetter == null)
                return;

            _mobileCanvas.Disable();
            _desktopCanvas.Disable();

            if (YG2.envir.isMobile)
            {
                UseMobileMode();
                _languageViewMobile.Initialize(languageSetter);
                _itemFactory.SetContentTransform(_mobileContent);
            }
            else
            {
                UseDesktopMode();
                _languageViewDesktop.Initialize(languageSetter);
                _itemFactory.SetContentTransform(_desktopContent);
            }
        }

        public void UseMobileMode()
        {
            _mobileCanvas.Enable();
        }

        public void UseDesktopMode()
        {
            _desktopCanvas.Enable();
        }
    }
}