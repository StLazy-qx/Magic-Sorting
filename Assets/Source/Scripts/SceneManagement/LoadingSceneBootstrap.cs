using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Scripts.SceneManagement
{
    public static class LoadingSceneBootstrap
    {
        private const int LoadingSceneIndex = 2;
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
                return;

            if (SceneManager.GetSceneByBuildIndex(LoadingSceneIndex).isLoaded)
                return;

            _initialized = true;

            SceneManager.LoadSceneAsync(
                LoadingSceneIndex,
                LoadSceneMode.Additive
                ).completed += operation =>
                {
                    Scene loadingScene =
                    SceneManager.GetSceneByBuildIndex(LoadingSceneIndex);

                    foreach (GameObject root in loadingScene.GetRootGameObjects())
                    {
                        Object.DontDestroyOnLoad(root);
                    }
                };
        }
    }
}
