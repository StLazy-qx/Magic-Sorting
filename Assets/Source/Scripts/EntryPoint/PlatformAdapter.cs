using UnityEngine;
using FactoryCore;
using YG;
using System;

namespace EntryPoint
{
    public abstract class PlatformAdapter : MonoBehaviour
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
            ValidateRequiredObjects();
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

        private void ValidateRequiredObjects()
        {
            if (_mobileCanvas == null)
                throw new ArgumentNullException(nameof(_mobileCanvas));

            if (_desktopCanvas == null)
                throw new ArgumentNullException(nameof(_desktopCanvas));

            if (_itemFactory == null)
                throw new ArgumentNullException(nameof(_itemFactory));

            if (_mobileContent == null)
                throw new ArgumentNullException(nameof(_mobileContent));

            if (_desktopContent == null)
                throw new ArgumentNullException(nameof(_desktopContent));
        }
    }
}
