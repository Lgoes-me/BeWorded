public interface IProduct
{
    int Price { get; }
}

public class PowerUpProduct : IProduct
{
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
    public int Price { get; }
    public string Identifier { get; }

    public JokerProduct(int price, string identifier)
    {
        Price = price;
        Identifier = identifier;
    }
}