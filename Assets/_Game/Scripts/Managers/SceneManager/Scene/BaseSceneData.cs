using UnityEngine;

public abstract class BaseSceneData : ScriptableObject
{
    public abstract string SceneName { get; }
    public Application Application { get; private set; }

    public void Init(Application application)
    {
        Application = application;
    }
}
