using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VesselStateTracker : MonoBehaviour
{
    private const float TimeEndSession = 1.7f;

    [SerializeField] private Panel _finalMatchPanelDesctop;
    [SerializeField] private Panel _finalMatchPanelMobile;
    [SerializeField] private FinalGameSession _finalGame;
    [SerializeField] private ParticlePool _particlePool;

    private Wallet _wallet;
    private Panel _currentPanel;
    private int _veselsCount;
    private IReadOnlyList<Vessel> _vessels;
    private WaitForSeconds _waitForEndSession = new(TimeEndSession);

    private void OnDestroy()
    {
        foreach (Vessel vessel in _vessels)
            vessel.ScoreUpdated -= OnPerfomeEffectCoroutine;
    }

    [Inject]
    public void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Initialize(IReadOnlyList<Vessel> vessels)
    {
        _vessels = vessels;

        foreach (Vessel vessel in _vessels)
            vessel.ScoreUpdated += OnPerfomeEffectCoroutine;

        _particlePool.Initialize(vessels.Count);
    }

    public void UseDesctopPanel()
    {
        _currentPanel = _finalMatchPanelDesctop;
    }

    public void UseMobilePanel()
    {
        _currentPanel = _finalMatchPanelMobile;
    }

    private void OnPerfomeEffectCoroutine(Vector3 position, int value, Color color)
    {
        StartCoroutine(OnPerfomeEffect(position, value, color));
    }

    //волшебные числа и поработать с ответсвенностью
    private IEnumerator OnPerfomeEffect(Vector3 position, int value, Color color)
    {
        ParticleSystem particle = _particlePool.Get();

        particle.transform.position = new Vector3(position.x, position.y - 0.3f, position.z);
        ParticleSystem.MainModule main = particle.main;
        main.startColor = color;

        particle.Play();

        yield return _waitForEndSession;

        OnAddPoints(value);
    }

    private void OnAddPoints(int value)
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
