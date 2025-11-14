using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
        foreach (Vessel vessel in _vessels)
            vessel.PointsEarned -= OnPerformEffectCoroutine;
    }

    [Inject]
    public void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Initilize()
    {
        if (IsInitialized)
            return;

        if (_vessels == null || _vessels.Count == 0)
            return;

        if (_effecter == null)
            return;

        if (_currentPanel == null)
            return;

        if (_wallet == null)
            return;

        IsInitialized = true;
    }

    public void SetVesselsList(IReadOnlyList<Vessel> vessels)
    {
        _vessels = vessels;

        foreach (Vessel vessel in _vessels)
            vessel.PointsEarned += OnPerformEffectCoroutine;

        _effecter.Initialize(vessels.Count);
    }

    public void ApplyPanel(Panel panel)
    {
        _currentPanel = panel ?? throw new ArgumentNullException(nameof(panel),
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
            _currentPanel.Close();
            _finalGame.ActivateFinalPanelAndPauseGame();

            _veselsCount = 0;
        }
    }
}
