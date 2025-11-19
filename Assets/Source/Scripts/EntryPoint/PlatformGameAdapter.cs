using UnityEngine;
using GameBehaviour;
using Vessels;
using System;

namespace EntryPoint
{
    public class PlatformGameAdapter : PlatformAdapter
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
            ValidateRequiredDependencies();
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

        private void ValidateRequiredDependencies()
        {
            if (_desktopObjectsPosition == null)
                throw new ArgumentNullException(nameof(_desktopObjectsPosition));

            if (_mobileObjectsPosition == null)
                throw new ArgumentNullException(nameof(_mobileObjectsPosition));

            if (_finalMatchPanelDesktop == null)
                throw new ArgumentNullException(nameof(_finalMatchPanelDesktop));

            if (_finalMatchPanelMobile == null)
                throw new ArgumentNullException(nameof(_finalMatchPanelMobile));

            if (_vesselsFulling == null)
                throw new ArgumentNullException(nameof(_vesselsFulling));

            if (_finalGameSession == null)
                throw new ArgumentNullException(nameof(_finalGameSession));
        }
    }
}