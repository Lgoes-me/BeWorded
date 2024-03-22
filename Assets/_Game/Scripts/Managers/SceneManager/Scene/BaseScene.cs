using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected abstract BaseSceneData BaseSceneData { get; }
    
    protected Application Application => BaseSceneData.Application;
    
    private void Awake()
    {
        Application.SceneManager.SetScene(this);
    }
}

public abstract class BaseScene<T> : BaseScene where T : BaseSceneData
{
    protected override BaseSceneData BaseSceneData => SceneData;
    [field: SerializeField] protected T SceneData { get; private set; }
}