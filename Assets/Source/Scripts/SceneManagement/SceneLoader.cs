using UnityEngine;
using UnityEngine.SceneManagement;

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
        //добавить исключение
        if (index < 0 && index > _scenes.Length)
            return;

        SceneManager.LoadScene(_scenes[index]);
    }

    public void LoadSceneByName(string name)
    {
        //добавить исключение по строке
        if (System.Array.Exists(_scenes, scene => scene == name) == false)
            return;

        SceneManager.LoadScene(name);
    }
}
