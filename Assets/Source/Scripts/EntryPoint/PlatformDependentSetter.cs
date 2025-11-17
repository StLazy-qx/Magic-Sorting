using UnityEngine;
using GameBehaviour;
using Vessels;

namespace EntryPoint
{
    public class PlatformDependentSetter : PlatformDependentBase
    {
        [Header("Begin Objects Position")]
        [SerializeField] private ObjectsBeginPositionSetter _desktopObjectsPosition;
        [SerializeField] private ObjectsBeginPositionSetter _mobileObjectsPosition;

        [Header("Panels")]
        [SerializeField] private Panel _finalMatchPanelDesktop;
        [SerializeField] private Panel _finalMatchPanelMobile;

        [Header("Links")]
        [SerializeField] private VesselStateTracker _vesselsFulling;
        [SerializeField] private FinalGameSession _finalGameSession;

        public void Initialize()
        {
            InitializeBase();
        }

        protected override void OnMobileSelected()
        {
            _mobileObjectsPosition.Initialize();
            _vesselsFulling.ApplyPanel(_finalMatchPanelMobile);
            _finalGameSession.ApplyPanel(_finalMatchPanelMobile);
        }

        protected override void OnDesktopSelected()
        {
            _desktopObjectsPosition.Initialize();
            _vesselsFulling.ApplyPanel(_finalMatchPanelDesktop);
            _finalGameSession.ApplyPanel(_finalMatchPanelDesktop);
        }
    }
}