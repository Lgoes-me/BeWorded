using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected abstract BaseSceneData BaseSceneData { get; } 
    
    private void Awake()
    {
        BaseSceneData.SceneManager.SetScene(this);
    }
}

public abstract class BaseScene<T> : BaseScene where T : BaseSceneData
{
    protected override BaseSceneData BaseSceneData => SceneData;
    [field: SerializeField] protected T SceneData { get; private set; }
}