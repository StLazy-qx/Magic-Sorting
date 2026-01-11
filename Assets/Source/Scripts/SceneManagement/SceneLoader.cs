using Assets.Source.Scripts.EntryPoint;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Scripts.SceneManagement
{
    public class SceneLoader
    {
        private const int GameSceneIndex = 1;
        private const int MainMenuIndex = 0;

        public void LoadMainMenu()
        {
            LoadScene(MainMenuIndex);
        }

        public void LoadGame()
        {
            LoadScene(GameSceneIndex);
        }

        private void LoadScene(int sceneIndex)
        {
            ValidateIndex(sceneIndex);

            LoadingWindow.Instance.Show();

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.completed += _ =>
            {
                if (LoadingWindow.Instance != null)
                    LoadingWindow.Instance.Hide();
            };
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= SceneManager.sceneCountInBuildSettings)
                throw new ArgumentOutOfRangeException(nameof(index));
        }
    }
}