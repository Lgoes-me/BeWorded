using UnityEngine;

[CreateAssetMenu]
public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "GameplayScene";
    public ContentManager ContentManager { get; private set; }
    public GameConfig GameConfig { get; private set; }

    public void Init(SceneManager sceneManager, ContentManager contentManager, GameConfig gameConfig)
    {
        base.Init(sceneManager);
        ContentManager = contentManager;
        GameConfig = gameConfig;
    }
}