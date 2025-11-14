using UnityEngine;
using YG;
using GameBehaviour;
using Vessels;

namespace EntryPoint
{
    public class PlatformDependentSetter : MonoBehaviour
    {
        [Header("Canvas Setters")]
        [SerializeField] private CanvasMobileSetter _mobileCanvas;
        [SerializeField] private CanvasDesktopSetter _desktopCanvas;
        [Header("Position Setters")]
        [SerializeField] private ObjectsBeginPositionSetter _desktopObjectsPosition;
        [SerializeField] private ObjectsBeginPositionSetter _mobileObjectsPosition;
        [Header("Panels")]
        [SerializeField] private Panel _finalMatchPanelDesctop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [Header("Links")]
        [SerializeField] private VesselStateTracker _vesselsFulling;
        [SerializeField] private FinalGameSession _finalGameSession;

        public void Initilize()
        {
            _mobileCanvas.Disable();
            _desktopCanvas.Disable();

            if (YG2.envir.isMobile)
            {
                UseMobileMode();
            }
            else
            {
                UseDesktopMode();
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