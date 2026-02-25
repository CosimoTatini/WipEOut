using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] string _sceneName = "";

    public void LoadSceneByName()
    {
        if(!string.IsNullOrEmpty(_sceneName))
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
