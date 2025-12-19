using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Assets.Source.Scripts.SceneManagement
{
    public class SceneLoader
    {
        private string[] _scenes;
        //private AsyncOperation _loadingOperation;

        //private void Awake()
        //{
        //    int sceneCount = SceneManager.sceneCountInBuildSettings;
        //    _scenes = new string[sceneCount];

        //    for (int i = 0; i < sceneCount; i++)
        //    {
        //        string path = SceneUtility.GetScenePathByBuildIndex(i);
        //        string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
        //        _scenes[i] = sceneName;
        //    }
        //}

        public SceneLoader()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            _scenes = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                _scenes[i] = Path.GetFileNameWithoutExtension(path);
            }
        }

        public void LoadSceneByIndex(int index)
        {
            ValidateIndex(index);
            SceneManager.LoadScene(_scenes[index]);
        }

        public void LoadSceneAdditive(int index)
        {
            ValidateIndex(index);
            SceneManager.LoadScene(_scenes[index], LoadSceneMode.Additive);
        }

        private void ValidateIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index >= _scenes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Index {index} out of range (0–{_scenes.Length - 1})");
            }
        }

        //public void LoadSceneByIndex(int targetSceneIndex)
        //{
        //    if (targetSceneIndex < 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(targetSceneIndex),
        //            "Scene index cannot be negative");
        //    }

        //    if (targetSceneIndex >= _scenes.Length)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(targetSceneIndex),
        //            $"Scene index {targetSceneIndex} is out of range." +
        //            $" Available scenes: 0-{_scenes.Length - 1}");
        //    }

        //    SceneManager.LoadScene(_scenes[targetSceneIndex]);
        //    //StartCoroutine(LoadSceneAsync(targetSceneIndex));
        //}

        //private IEnumerator LoadSceneAsync(int targetSceneIndex)
        //{
        //    _loadingOperation =
        //        SceneManager.LoadSceneAsync(targetSceneIndex);
        //    _loadingOperation.allowSceneActivation = false;

        //    while (!_loadingOperation.isDone)
        //    {
        //        if (_loadingOperation.progress >= 0.9f)
        //        {
        //            yield return new WaitForSeconds(0.5f);
        //            _loadingOperation.allowSceneActivation = true;
        //        }

        //        yield return null;
        //    }
        //}
    }
}