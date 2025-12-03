using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Source.Scripts.EntryPoint;
using Assets.Source.Scripts.GameBehaviour;
using Assets.Source.Scripts.Player;
using System.Linq;
using Assets.Source.Scripts.UI.GamePanel;

namespace Assets.Source.Scripts.Vessels
{
    public class VesselStateTracker : MonoBehaviour, IObjectInitilizable
    {
        private const float TimeEndSession = 1.7f;

        [SerializeField] private Panel _finalMatchPanelDesctop;
        [SerializeField] private Panel _finalMatchPanelMobile;
        [SerializeField] private FinalGameSession _finalGame;
        [SerializeField] private VesselCompletionEffecter _effecter;

        private Wallet _wallet;
        private Panel _currentPanel;
        private int _veselsCount;
        private IReadOnlyList<Vessel> _vessels;

        public bool IsInitialized { get; private set; }

        private void OnDestroy()
        {
            if (_vessels == null)
                return;

            foreach (Vessel vessel in _vessels)
                vessel.RewardIssued -= OnPerformEffectCoroutine;
        }

        [Inject]
        public void Construct(Wallet wallet)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet),
                "[VesselStateTracker] Wallet cannot be null");
        }

        public void Initialize()
        {
            if (IsInitialized)
                return;

            ValidateInitializeArguments();

            IsInitialized = true;
        }

        public void SetVesselsList(IReadOnlyList<Vessel> vessels)
        {
            ValidateVesselsList(vessels);

            _vessels = vessels;

            foreach (Vessel vessel in _vessels)
                vessel.RewardIssued += OnPerformEffectCoroutine;

            _effecter.Initialize(vessels.Count);
        }

        public void ApplyPanel(Panel panel)
        {
            _currentPanel = panel ??
                throw new ArgumentNullException(nameof(panel),
                "[VesselStateTracker] Панель не может быть нуль");
        }

        private void OnPerformEffectCoroutine(Vector3 position, int value, Color color)
        {
            StartCoroutine(PerformEffect(position, value, color));
        }

        private IEnumerator PerformEffect(Vector3 position, int value, Color color)
        {
            yield return _effecter.PlayEffect(position, color, TimeEndSession);

            OnFixateVessel(value);
        }

        private void OnFixateVessel(int value)
        {
            _wallet.AddPoints(value);

            _veselsCount++;

            if (_veselsCount == _vessels.Count)
            {
                _wallet.ConfirmPoints();
                _wallet.Reset();
                _currentPanel.Close();
                _finalGame.ActivateFinalPanelAndPauseGame();

                _veselsCount = 0;
            }
        }

        private void ValidateInitializeArguments()
        {
            if (_wallet == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] Wallet reference is missing. " +
                    "Did you forget Inject()?");
            }

            if (_effecter == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] Effecter reference is missing in inspector.");
            }


            if (_finalGame == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] FinalGameSession reference is missing.");
            }

            if (_finalMatchPanelDesctop == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] FinalMatchPanelDesktop is missing.");
            }

            if (_finalMatchPanelMobile == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] FinalMatchPanelMobile is missing.");
            }

            if (_vessels == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] Vessel list is not assigned. " +
                    "Call SetVesselsList() first.");
            }

            if (_vessels.Count == 0)
            {
                throw new ArgumentException(
                    "[VesselStateTracker] Vessel list is empty.");
            }

            if (_vessels.Any(vessel => vessel == null))
            {
                throw new ArgumentException(
                    "[VesselStateTracker] Vessel list contains null entries.");
            }

            if (_currentPanel == null)
            {
                throw new NullReferenceException(
                    "[VesselStateTracker] Panel is not assigned. Call ApplyPanel() first.");
            }
        }

        private void ValidateVesselsList(IReadOnlyList<Vessel> vessels)
        {
            if (vessels == null)
            {
                throw new ArgumentNullException(nameof(vessels),
                    "[VesselStateTracker] Vessel list cannot be null");
            }

            if (vessels.Count == 0)
            {
                throw new ArgumentException(
                    "[VesselStateTracker] Vessel list cannot be empty",
                    nameof(vessels));
            }

            if (vessels.Any(vessel => vessel == null))
            {
                throw new ArgumentException(
                    "[VesselStateTracker] Vessel list contains null entries",
                    nameof(vessels));
            }
        }
    }
}