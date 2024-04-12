using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [field: SerializeField] private Button BuyItemButton { get; set; }
    [field: SerializeField] private Button ShowInfoButton { get; set; }
    [field: SerializeField] private TextController ItemName { get; set; }
    [field: SerializeField] private TextMeshProUGUI ItemPrice { get; set; }

    private IProduct Product { get; set; }
    private ShopScene ShopScene { get; set; }
    private AlertManager AlertManager { get; set; }
    private TextManager TextManager { get; set; }

    public ShopItemController Init(
        IProduct product,
        ShopScene shopScene,
        AlertManager alertManager,
        TextManager textManager)
    {
        Product = product;
        ShopScene = shopScene;
        AlertManager = alertManager;
        TextManager = textManager;

        BuyItemButton.onClick.AddListener(BuyProduct);
        ShowInfoButton.onClick.AddListener(ShowInfo);

        ItemName.Init(product.ProductName, TextManager);
        ItemPrice.SetText(product.Price.ToString());

        return this;
    }

    private void BuyProduct()
    {
        if (ShopScene.TryBuyProduct(Product))
            Destroy(this.gameObject);

        transform.DOShakePosition(0.1f, 2);
    }

    private void ShowInfo()
    {
        AlertManager.ShowTooltip(TextManager.GetString(Product.ProductInfo),
            new List<RectTransform>() {(RectTransform) transform});
    }
}