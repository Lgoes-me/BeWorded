using System;

public abstract class ScoreModifier
{
    public abstract void DoModification(ref int basePrize, ref int baseMultiplier);
}

public class ExtraPrizeScoreModifier : ScoreModifier
{
    private int ExtraPrize { get; set; }

    public ExtraPrizeScoreModifier(int extraPrize)
    {
        ExtraPrize = extraPrize;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        basePrize += ExtraPrize;
    }
}

public class ExtraMultiplierScoreModifier : ScoreModifier
{
    private int ExtraMultiplier { get; set; }

    public ExtraMultiplierScoreModifier(int extraMultiplier)
    {
        ExtraMultiplier = extraMultiplier;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        baseMultiplier += ExtraMultiplier;
    }
}

public class MultiplyMultiplierScoreModifier : ScoreModifier
{
    private int Multiplier { get; set; }

    public MultiplyMultiplierScoreModifier(int multiplier)
    {
        Multiplier = multiplier;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        baseMultiplier *= Multiplier;
    }
}

public class ExtraPrizeAndMultiplierScoreModifier : ScoreModifier
{
    private int ExtraPrize { get; set; }
    private int ExtraMultiplier { get; set; }

    public ExtraPrizeAndMultiplierScoreModifier(int extraPrize, int extraMultiplier)
    {
        ExtraPrize = extraPrize;
        ExtraMultiplier = extraMultiplier;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        basePrize += ExtraPrize;
        baseMultiplier += ExtraMultiplier;
    }
}

public class MoneyPerCharacterScoreModifier : ScoreModifier
{
    private Player Player { get; set; }
    private int Money { get; set; }

    public MoneyPerCharacterScoreModifier(Player player, int money)
    {
        Player = player;
        Money = money;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        Player.GivePrize(Money);
    }
}

public class PowerUpPerScoreModifier : ScoreModifier
{
    private Player Player { get; set; }
    private PowerUpType PowerUpType { get; set; }
    private int Quantity { get; set; }

    public PowerUpPerScoreModifier(Player player, PowerUpType powerUpType, int quantity)
    {
        Player = player;
        PowerUpType = powerUpType;
        Quantity = quantity;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        switch (PowerUpType)
        {
            case PowerUpType.Troca:
                Player.Swaps.Gain(Quantity);
                break;
            case PowerUpType.Bomba:
                Player.Bombs.Gain(Quantity);
                break;
            case PowerUpType.Misturar:
                Player.Shuffles.Gain(Quantity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class ScoreCounterModifier : ScoreModifier
{
    private Player Player { get; set; }
    private JokerIdentifier Identifier { get; set; }

    public ScoreCounterModifier(Player player, JokerIdentifier identifier)
    {
        Player = player;
        Identifier = identifier;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        if (Player.JokersExtraParams.TryGetValue(Identifier, out var value))
        {
            Player.JokersExtraParams[Identifier] = value + 1;
        }
        else
        {
            Player.JokersExtraParams.Add(Identifier, 1);
        }
    }
}

public class ExtraMultiplierFromCounterScoreModifier : ScoreModifier
{
    private Player Player { get; set; }
    private JokerIdentifier Identifier { get; set; }

    public ExtraMultiplierFromCounterScoreModifier(Player player, JokerIdentifier identifier)
    {
        Player = player;
        Identifier = identifier;
    }

    public override void DoModification(ref int basePrize, ref int baseMultiplier)
    {
        if (Player.JokersExtraParams.TryGetValue(Identifier, out var value))
        {
            baseMultiplier += value;
        }
    }
}