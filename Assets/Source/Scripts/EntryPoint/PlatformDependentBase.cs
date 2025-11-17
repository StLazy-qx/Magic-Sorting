using UnityEngine;
using FactoryCore;
using YG;

namespace EntryPoint
{
    public abstract class PlatformDependentBase : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] protected CanvasMobileSetter _mobileCanvas;
        [SerializeField] protected CanvasDesktopSetter _desktopCanvas;
        [Header("Store installation")]
        [SerializeField] protected StoreItemFactory _itemFactory;
        [SerializeField] protected Transform _desktopContent;
        [SerializeField] protected Transform _mobileContent;

        protected void InitializeBase()
        {
            _mobileCanvas.Disable();
            _desktopCanvas.Disable();

            if (YG2.envir.isMobile)
            {
                UseMobileMode();
                _itemFactory.SetContentTransform(_mobileContent);
                OnMobileSelected();
            }
            else
            {
                UseDesktopMode();
                _itemFactory.SetContentTransform(_desktopContent);
                OnDesktopSelected();
            }
        }

        protected virtual void UseMobileMode()
        {
            _mobileCanvas.Enable();
        }

        protected virtual void UseDesktopMode()
        {
            _desktopCanvas.Enable();
        }

        protected abstract void OnMobileSelected();

        protected abstract void OnDesktopSelected();
    }
}
