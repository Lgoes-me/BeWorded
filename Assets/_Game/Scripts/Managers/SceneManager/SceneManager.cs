using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private BaseScene MainScene { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeMainScene(BaseSceneData data)
    {
        UnitySceneManager.LoadScene(data.SceneName, LoadSceneMode.Single);
    }
    
    public void SetScene(BaseScene scene)
    {
        MainScene = scene;
    }
}
