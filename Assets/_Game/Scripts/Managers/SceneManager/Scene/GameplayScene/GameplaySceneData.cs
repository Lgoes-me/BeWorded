using UnityEngine;

[CreateAssetMenu]
public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "GameplayScene";
    public Player Player { get; private set; }
    public LevelConfig LevelConfig { get; private set; }

    public void Init(Application application, Player player)
    {
        base.Init(application);
        Player = player;
        
        PrepareNextLevel();
    }

    public void PrepareNextLevel()
    {
        LevelConfig = Application.ConfigManager.GetNextLevelConfig(Player);
    }
}