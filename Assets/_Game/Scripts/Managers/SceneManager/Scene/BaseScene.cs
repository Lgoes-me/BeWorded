using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected abstract BaseSceneData BaseSceneData { get; }
    
    protected SceneManager SceneManager => BaseSceneData.Application.SceneManager;
    protected ContentManager ContentManager => BaseSceneData.Application.ContentManager;
    
    private void Awake()
    {
        SceneManager.SetScene(this);
    }
}

public abstract class BaseScene<T> : BaseScene where T : BaseSceneData
{
    protected override BaseSceneData BaseSceneData => SceneData;
    [field: SerializeField] protected T SceneData { get; private set; }
}