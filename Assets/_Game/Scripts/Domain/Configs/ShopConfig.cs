using System.Collections.Generic;

public class ShopConfig
{
    public List<IProduct> GetProducts(JokerFactory jokerFactory) => new()
    {
        new PowerUpProduct(2, PowerUpType.Troca),
        new PowerUpProduct(2, PowerUpType.Bomba),
        new PowerUpProduct(3, PowerUpType.Misturar),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.BaseA)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.BaseVogal)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MultM)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MultiplyZ)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MoneyS)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.PowerUpBombB)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MultSimpleWord)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MultiplySimpleWord)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.EscalatingJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.Joker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.GreedyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.LustyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.WrathfulJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.GluttonousJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.PridefulJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.JollyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.ZanyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.MadJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.SlyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.WillyJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.CleverJoker)),
        new JokerProduct(3, jokerFactory.CreateJoker(JokerIdentifier.HalfJoker)),
    };
}