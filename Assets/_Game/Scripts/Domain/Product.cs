public interface IProduct
{
    string ProductName { get; }
    string ProductInfo => $"{ProductName}_info";
    int Price { get; }
}

public class PowerUpProduct : IProduct
{
    public string ProductName => PowerUp.ToString();
    public int Price { get; }
    public PowerUpType PowerUp { get; }

    public PowerUpProduct(int price, PowerUpType powerUp)
    {
        Price = price;
        PowerUp = powerUp;
    }
}

public class JokerProduct : IProduct
{
    public string ProductName => Joker.Id;
    public int Price { get; }
    public Joker Joker { get; }

    public JokerProduct(int price, Joker joker)
    {
        Price = price;
        Joker = joker;
    }
}