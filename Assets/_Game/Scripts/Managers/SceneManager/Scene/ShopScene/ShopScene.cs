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

    private void Start()
    {
        Continue.onClick.AddListener(() => Application.SceneManager.OpenGameplayScene(SceneData.Player));

        var jokerFactory = new JokerFactory(SceneData.Player);
        var randomProducts = 
            Application.ConfigManager.ShopConfig.GetProducts(jokerFactory).RandomElementList(3, 123456);

        foreach (var product in randomProducts)
        {
            Instantiate(ShopItemControllerPrefab, ProductsContent).Init(product, this);
        }
        
        PlayerMoney.SetText(SceneData.Player.Money.ToString());
    }

    public bool TryBuyProduct(IProduct product)
    {
        var player = SceneData.Player;

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

                player.Jokers.Add(jokerProduct.Joker);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        player.Money -= product.Price;
        PlayerMoney.SetText(SceneData.Player.Money.ToString());
        
        return true;
    }
}