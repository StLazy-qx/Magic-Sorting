using Assets.Source.Scripts.UI.GamePanel;
using Assets.Source.Scripts.ActionsHandlers;
using Assets.Source.Scripts.GameBehaviour;
using Assets.Source.Scripts.Vessels;
using UnityEngine;
using System;
using Assets.Source.Scripts.UI.Buttons;

namespace Assets.Source.Scripts.EntryPoint
{
    public class PlatformGameAdapter : PlatformAdapter
    {
        [Header("Begin Objects Position")]
        [SerializeField] private ObjectsBeginPositionSetter _desktopObjectsPosition;
        [SerializeField] private ObjectsBeginPositionSetter _mobileObjectsPosition;
        [Header("UI Elements")]
        [SerializeField] private Panel _finalMatchPanelDesktop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [SerializeField] private ReverseButton _reverseButtonDesktop;
        [SerializeField] private ReverseButton _reverseButtonMobile;
        [Header("Links")]
        [SerializeField] private VesselStateTracker _vesselsFulling;
        [SerializeField] private FinalGameSession _finalGameSession;
        [SerializeField] private ClickImpactHandler _clickImpactHandler;

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
            _clickImpactHandler.SetButton(_reverseButtonMobile);
        }

        protected override void OnDesktopSelected()
        {
            _desktopObjectsPosition.Initialize();
            _vesselsFulling.ApplyPanel(_finalMatchPanelDesktop);
            _finalGameSession.ApplyPanel(_finalMatchPanelDesktop);
            _clickImpactHandler.SetButton(_reverseButtonDesktop);
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