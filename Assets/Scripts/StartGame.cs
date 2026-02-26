using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is linked to a button . Given a scene name, it loads a new scene. 
/// </summary>

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
