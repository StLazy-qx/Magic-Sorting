using System;
using UnityEngine;
using Zenject;
using GameDifficulty;
using EntryPoint;
using PlayerCore;

namespace GameBehaviour
{
    public abstract class BaseGameHandler : MonoBehaviour, IObjectInitilizable, IGameHandler
    {
        protected readonly int MainMenuIndex = 0;
        protected readonly int GameSessionIndex = 1;
        protected readonly int GamePause = 0;
        protected readonly int GameResume = 1;

        [SerializeField] protected SceneLoader _sceneLoader;

        protected Wallet _wallet;
        protected DifficultyState _difficultyState;

        public bool IsInitialized { get; protected set; }

        public event Action<bool> PauseStateChanged;
        public event Action GameClosed;

        public virtual void Initilize()
        {
            if (IsInitialized)
                return;

            Time.timeScale = GameResume;

            _difficultyState.SetDifficulty(DifficultyLevel.Easy);
            _wallet.Reset();
            ExtendInitialize();

            IsInitialized = true;
        }

        public virtual void ContinueGame()
        {
            PauseStateChanged?.Invoke(false);

            Time.timeScale = GameResume;
        }

        public virtual void PauseGame()
        {
            Time.timeScale = GamePause;

            PauseStateChanged?.Invoke(true);
        }

        public virtual void ResumeGame()
            => NavigateScene(GameSessionIndex);

        public virtual void OpenMainMenu()
            => NavigateScene(MainMenuIndex, true);

        public virtual void QuitGame()
        {
            GameClosed?.Invoke();
            Application.Quit();
        }

        protected virtual void ExtendInitialize() { }

        protected DifficultyLevel GetIncreasedDifficulty(DifficultyLevel current)
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

        [Inject]
        private void Construct(Wallet wallet, DifficultyState difficultyState)
        {
            _wallet = wallet;
            _difficultyState = difficultyState;
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
}
