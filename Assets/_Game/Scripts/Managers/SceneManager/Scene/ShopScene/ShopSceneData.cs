using UnityEngine;

[CreateAssetMenu]
public class ShopSceneData : BaseSceneData
{
    public override string SceneName => "ShopScene";
    public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }
}