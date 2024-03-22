using UnityEngine;

[CreateAssetMenu]
public class GameplaySceneData : BaseSceneData
{
    public override string SceneName => "GameplayScene";
    public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }
}