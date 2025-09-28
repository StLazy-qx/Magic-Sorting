using System;
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

    private Wallet _wallet;
    private DifficultyState _difficultyState;

    public event Action<bool> PauseStateChanged;

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

    public void BegintNewRound()
    {
        ContinueGame();

        _vesselFactory.ResetFactory(_difficultyState.CurrentDifficulty);
        _columnsFactory.ResetFactory(_difficultyState.CurrentDifficulty);
        _wallet.Reset();
    }

    public void IncreaseDifficultyLevel()
    {
        Time.timeScale = GameResume;

        DifficultyLevel current = _difficultyState.CurrentDifficulty;
        DifficultyLevel newLevel = GetIncreasedDifficulty(current);

        if (newLevel != current)
            _difficultyState.SetDifficulty(newLevel);

        _vesselFactory.ResetFactory(newLevel);
        _columnsFactory.ResetFactory(newLevel);
        _wallet.Reset();
    }

    public void ResumeGame()
    {
        ContinueGame();
        _sceneLoader.LoadSceneByIndex(GameSessionIndex);
    }

    public void OpenMainMenu()
    {
        PauseGame();
        _sceneLoader.LoadSceneByIndex(MainMenuIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры...");

        Time.timeScale = GamePause;

        //здесь выход из игры
        //Application.Quit();
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
}
