using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameHandler : MonoBehaviour
{
    private readonly int MainMenuIndex = 0;
    private readonly int GameSessionIndex = 1;
    private readonly int GamePause = 0;
    private readonly int GameResume = 1;

    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private ColumnsFactory _columnsFactory;
    [SerializeField] private VesselFactory _vesselFactory;
    [SerializeField] private WaitingPoint _waitingPoint;

    private Wallet _wallet;
    private DifficultyState _difficultyState;

    public event Action<bool> PauseStateChanged;
    public event Action GameClosed;

    [Inject]
    private void Construct(Wallet wallet, DifficultyState difficultyState)
    {
        _wallet = wallet;
        _difficultyState = difficultyState;
    }

    public void ContinueGame()
    {
        PauseStateChanged?.Invoke(false);
        Time.timeScale = GameResume;
    }

    public void PauseGame()
    {
        Time.timeScale = GamePause;
        PauseStateChanged?.Invoke(true);
    }

    public void BeginNewRound()
    {
        ContinueGame();
        _wallet.Reset();
        _waitingPoint.Reset();
        StartCoroutine(StartNewRoundRoutine());
    }

    public void IncreaseDifficultyLevel()
    {
        ContinueGame();

        DifficultyLevel current = _difficultyState.CurrentDifficulty;
        DifficultyLevel newLevel = GetIncreasedDifficulty(current);

        if (newLevel != current)
            _difficultyState.SetDifficulty(newLevel);

        ResetFactories();
        _wallet.Reset();
        StartCoroutine(StartNewRoundRoutine());
    }

    public void ResumeGame()
        => NavigateScene(GameSessionIndex);

    public void OpenMainMenu()
        => NavigateScene(MainMenuIndex, true);

    public void QuitGame()
    {
        GameClosed?.Invoke();

        Application.Quit();
    }

    private IEnumerator StartNewRoundRoutine()
    {
        _vesselFactory.ResetFactory(_difficultyState.CurrentDifficulty);
        _columnsFactory.ResetFactory(_difficultyState.CurrentDifficulty);
        _vesselFactory.Spawn();

        yield return new WaitUntil(() => _vesselFactory.IsReady);

        if (_vesselFactory.Objects != null &&
            _vesselFactory.Objects.Count > 0)
        {
            _columnsFactory.Initialize(_vesselFactory.Objects);
            _columnsFactory.Spawn();
        }
    }

    private DifficultyLevel GetIncreasedDifficulty(DifficultyLevel current)
    {
        switch (current)
        {
            case DifficultyLevel.Easy: 
                return DifficultyLevel.Medium;
            case DifficultyLevel.Medium: 
                return DifficultyLevel.Hard;
            case DifficultyLevel.Hard: 
                return DifficultyLevel.Hard;
            default: return current;
        }
    }

    private void ResetFactories()
    {
        _vesselFactory.ResetFactory(_difficultyState.CurrentDifficulty);
        _columnsFactory.ResetFactory(_difficultyState.CurrentDifficulty);
    }

    private void NavigateScene(int sceneIndex, bool isPause = false)
    {
        if (isPause)
            PauseGame();
        else
            ContinueGame();

        _sceneLoader.LoadSceneByIndex(sceneIndex);
    }
}
