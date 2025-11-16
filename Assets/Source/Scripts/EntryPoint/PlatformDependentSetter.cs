using UnityEngine;
using YG;
using GameBehaviour;
using Vessels;
using FactoryCore;

namespace EntryPoint
{
    public class PlatformDependentSetter : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private CanvasMobileSetter _mobileCanvas;
        [SerializeField] private CanvasDesktopSetter _desktopCanvas;
        [Header("Brgin Objects Position")]
        [SerializeField] private ObjectsBeginPositionSetter _desktopObjectsPosition;
        [SerializeField] private ObjectsBeginPositionSetter _mobileObjectsPosition;
        [Header("Panels")]
        [SerializeField] private Panel _finalMatchPanelDesctop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [Header("Links")]
        [SerializeField] private VesselStateTracker _vesselsFulling;
        [SerializeField] private FinalGameSession _finalGameSession;
        [Header("Store installation")]
        [SerializeField] private StoreItemFactory _itemFactory;
        [SerializeField] private Transform _desktopContent;
        [SerializeField] private Transform _mobileContent;

        public void Initilize()
        {
            _mobileCanvas.Disable();
            _desktopCanvas.Disable();

            if (YG2.envir.isMobile)
            {
                UseMobileMode();
                _itemFactory.SetContentTransform(_mobileContent);
            }
            else
            {
                UseDesktopMode();
                _itemFactory.SetContentTransform(_desktopContent);
            }
        }

        public void UseMobileMode()
        {
            _mobileCanvas.Enable();
            _mobileObjectsPosition.Initialize();
            _vesselsFulling.ApplyPanel(_finalMatchPanelMobile);
            _finalGameSession.ApplyPanel(_finalMatchPanelMobile);
        }

        public void UseDesktopMode()
        {
            _desktopCanvas.Enable();
            _desktopObjectsPosition.Initialize();
            _vesselsFulling.ApplyPanel(_finalMatchPanelDesctop);
            _finalGameSession.ApplyPanel(_finalMatchPanelDesctop);
        }
    }
}