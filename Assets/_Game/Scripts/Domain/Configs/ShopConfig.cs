using System.Collections.Generic;

public class ShopConfig
{
    public List<IProduct> GetProducts(JokerFactory jokerFactory) => new()
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
}