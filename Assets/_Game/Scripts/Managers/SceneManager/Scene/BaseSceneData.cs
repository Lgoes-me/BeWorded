using UnityEngine;

public abstract class BaseSceneData : ScriptableObject
{
    public abstract string SceneName { get; }
    public SceneManager SceneManager { get; private set; }

    protected void Init(SceneManager sceneManager)
    {
        SceneManager = sceneManager;
    }
}
