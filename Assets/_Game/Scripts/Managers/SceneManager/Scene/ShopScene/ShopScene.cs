using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScene : BaseScene<ShopSceneData>
{
    [field: SerializeField] private Button Continue { get; set; }

    private List<IProduct> GetProducts() => new()
    {
        new PowerUpProduct(2, PowerUpType.Troca),
        new PowerUpProduct(2, PowerUpType.Bomba),
        new PowerUpProduct(3, PowerUpType.Misturar),
        new JokerProduct(3, JokerIdentifier.BaseA),
        new JokerProduct(5, JokerIdentifier.BaseVogal),
        new JokerProduct(6, JokerIdentifier.MultM),
        new JokerProduct(7, JokerIdentifier.MultiplyZ),
        new JokerProduct(3, JokerIdentifier.MoneyS),
        new JokerProduct(4, JokerIdentifier.PowerUpBombB),
        new JokerProduct(3, JokerIdentifier.MultSimpleWord),
        new JokerProduct(8, JokerIdentifier.MultiplySimpleWord),
    };
    
    private void Start()
    {
        Continue.onClick.AddListener(() => Application.SceneManager.OpenGameplayScene(SceneData.Player));
        var randomProducts = GetProducts().RandomElementList(3);
    }
}