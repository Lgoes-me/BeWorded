using UnityEngine;

public abstract class BaseSceneData : ScriptableObject
{
    public abstract string SceneName { get; }
    public Application Application { get; private set; }

    protected void Init(Application application)
    {
        Application = application;
    }
}
