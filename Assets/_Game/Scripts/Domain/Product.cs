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