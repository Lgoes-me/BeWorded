using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : BaseScene<ShopSceneData>
{
    [field: SerializeField] private ShopItemController ShopItemControllerPrefab { get; set; }
    [field: SerializeField] private Transform ProductsContent { get; set; }
    [field: SerializeField] private Button Continue { get; set; }

    private List<IProduct> GetProducts(JokerFactory jokerFactory) => new()
    {
        new PowerUpProduct(2, PowerUpType.Troca),
        new PowerUpProduct(2, PowerUpType.Bomba),
        new PowerUpProduct(3, PowerUpType.Misturar),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.BaseA)),
        new JokerProduct(5, jokerFactory.CreateJoker(JokerIdentifier.BaseVogal)),
        new JokerProduct(6, jokerFactory.CreateJoker(JokerIdentifier.MultM)),
        new JokerProduct(7, jokerFactory.CreateJoker(JokerIdentifier.MultiplyZ)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MoneyS)),
        new JokerProduct(4, jokerFactory.CreateJoker(JokerIdentifier.PowerUpBombB)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MultSimpleWord)),
        new JokerProduct(8, jokerFactory.CreateJoker(JokerIdentifier.MultiplySimpleWord)),
    };

    private void Start()
    {
        Continue.onClick.AddListener(() => Application.SceneManager.OpenGameplayScene(SceneData.Player));

        var jokerFactory = new JokerFactory(SceneData.Player);
        var randomProducts = GetProducts(jokerFactory).RandomElementList(3);

        foreach (var product in randomProducts)
        {
            Instantiate(ShopItemControllerPrefab, ProductsContent).Init(product, this);
        }
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
                switch (powerUpProduct.PowerUp)
                {
                    case PowerUpType.Troca:
                        player.Swaps.Gain();
                        break;
                    case PowerUpType.Bomba:
                        player.Bombs.Gain();
                        break;
                    case PowerUpType.Misturar:
                        player.Shuffles.Gain();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

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

        return true;
    }
}