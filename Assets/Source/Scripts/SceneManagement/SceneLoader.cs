using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Source.Scripts.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private string[] _scenes;

        private void Awake()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            _scenes = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
                _scenes[i] = sceneName;
            }
        }

        public void LoadSceneByIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Scene index cannot be negative");
            }

            if (index >= _scenes.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"Scene index {index} is out of range." +
                    $" Available scenes: 0-{_scenes.Length - 1}");
            }

            SceneManager.LoadScene(_scenes[index]);
        }
    }
}

