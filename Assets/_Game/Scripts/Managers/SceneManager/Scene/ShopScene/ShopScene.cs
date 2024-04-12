using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : BaseScene<ShopSceneData>
{
    [field: SerializeField] private ShopItemController ShopItemControllerPrefab { get; set; }
    [field: SerializeField] private Transform ProductsContent { get; set; }
    [field: SerializeField] private TextMeshProUGUI PlayerMoney { get; set; }
    [field: SerializeField] private Button Continue { get; set; }

    private Player Player { get; set; }

    private void Start()
    {
        Player = Application.PlayerManager.Player;
        Continue.onClick.AddListener(Application.SceneManager.OpenGameplayScene);
        
        Player.OpenShop();
        var seed = Application.PlayerManager.Seed.GetNextShopItemSeed(Player.Shops);
        var jokerFactory = new JokerFactory(Player);
        var randomProducts = Application.ConfigManager.ShopConfig.GetProducts(jokerFactory).RandomElementList(3, seed);

        foreach (var product in randomProducts)
        {
            Instantiate(ShopItemControllerPrefab, ProductsContent)
                .Init(product, this, Application.AlertManager,Application.TextManager);
        }

        PlayerMoney.SetText(Player.Money.ToString());
    }

    public bool TryBuyProduct(IProduct product)
    {
        var player = Player;

        if (player.Money < product.Price)
            return false;

        switch (product)
        {
            case PowerUpProduct powerUpProduct:
            {
                player.GainPowerUp(powerUpProduct.PowerUp);
                break;
            }
            case JokerProduct jokerProduct:
            {
                if (player.Jokers.Count >= player.QuantidadeJokers)
                    return false;

                Application.GameEventsManager.SubscribeJoker(jokerProduct.Joker);
                player.Jokers.Add(jokerProduct.Joker);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        player.Money -= product.Price;
        PlayerMoney.SetText(Player.Money.ToString());

        return true;
    }
}