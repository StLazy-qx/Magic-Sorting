using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class VesselsFullingBehaviour : MonoBehaviour
{
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private FinalGameSession _finalGame;
    [SerializeField] private ParticleSystem _particlePrefab;

    private Wallet _wallet;
    private int _veselsCount;
    private IReadOnlyList<Vessel> _vessels;

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

    public void Init(IReadOnlyList<Vessel> vessels)
    {
        _vessels = vessels;

        foreach (Vessel vessel in _vessels)
            vessel.ScoreUpdated += OnPerfomeEffectCoroutine;
    }

    private void OnPerfomeEffectCoroutine(Vector3 position, int value, Color color)
    {
        StartCoroutine(OnPerfomeEffect(position, value, color));
    }

    //волшебные числа и поработать с ответсвенностью
    private IEnumerator OnPerfomeEffect(Vector3 position, int value, Color color)
    {
        ParticleSystem particle = Instantiate(
            _particlePrefab,
            new Vector3(position.x, position.y - 0.3f, position.z),
            Quaternion.Euler(-90f,0f,0f));

        ParticleSystem.MainModule main = particle.main;
        main.startColor = color;

        particle.Play();

        yield return new WaitForSeconds(1.7f);

        OnAddPoints(value);
    }

    private void OnAddPoints(int value)
    {
        _wallet.AddPoints(value);

        _veselsCount++;

        if (_veselsCount == _vessels.Count)
        {
            _wallet.ConfirmPoints();
            _gamePanel.Close();
            _finalGame.ActivateFinalPanelAndPauseGame();

            _veselsCount = 0;
        }
    }
}
