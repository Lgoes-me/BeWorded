﻿public interface IProduct
{
    string ProductName { get; }
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
    public BaseJoker Joker { get; }

    public JokerProduct(int price, BaseJoker joker)
    {
        Price = price;
        Joker = joker;
    }
}