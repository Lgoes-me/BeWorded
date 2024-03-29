using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [field: SerializeField] private Button BuyItemButton { get; set; }
    [field: SerializeField] private TextMeshProUGUI ItemName { get; set; }
    [field: SerializeField] private TextMeshProUGUI ItemPrice { get; set; }

    public ShopItemController Init(IProduct product, ShopScene shopScene)
    {
        BuyItemButton.onClick.AddListener(() =>
        {
            if (shopScene.TryBuyProduct(product))
                Destroy(this.gameObject);
            
            //Alerta de falta de dinheiro
        });

        ItemName.SetText(product.ProductName);
        ItemPrice.SetText(product.Price.ToString());
        
        return this;
    }
}