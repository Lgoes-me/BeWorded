using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : BaseManager
{
    private BaseScene MainScene { get; set; }

    public void ChangeMainScene(BaseSceneData data)
    {
        UnitySceneManager.LoadScene(data.SceneName, LoadSceneMode.Single);
    }
    
    public void SetScene(BaseScene scene)
    {
        MainScene = scene;
    }
}
