using System;
using Zenject;

public class DifficultyProvider : IInitializable, IDisposable
{
    private readonly DifficultyState _difficultyState;
    private readonly DifficultyDatabase _database;

    public DifficultySettings CurrentSettings { get; private set; }

    public event Action<DifficultySettings> OnSettingsChanged;

    public DifficultyProvider(DifficultyState state, DifficultyDatabase database)
    {
        _difficultyState = state;
        _database = database;
    }

    public void Initialize()
    {
        UpdateSettings(_difficultyState.CurrentDifficulty);
    }

    public void Dispose()
    {
        OnSettingsChanged = null;
    }

    private void UpdateSettings(DifficultyLevel level)
    {
        CurrentSettings = _database.GetSettings(level);

        OnSettingsChanged?.Invoke(CurrentSettings);
    }

    public void SetDifficulty(DifficultyLevel level)
    {
        _difficultyState.SetDifficulty(level);
        UpdateSettings(level);
    }
}
