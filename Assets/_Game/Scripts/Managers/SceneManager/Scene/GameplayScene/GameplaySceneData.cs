using UnityEngine;

[CreateAssetMenu]
public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "GameplayScene";
    public GameConfig GameConfig { get; private set; }

    public void Init(Application application, GameConfig gameConfig)
    {
        base.Init(application);
        GameConfig = gameConfig;
    }
}