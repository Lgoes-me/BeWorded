using System;
using System.Collections.Generic;

public class JokerFactory
{
    private Player Player { get; }

    public JokerFactory(Player player)
    {
        Player = player;
    }

    public Joker CreateJoker(JokerIdentifier id)
    {
        return id switch
        {
            JokerIdentifier.BaseA => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'A'})
                    .WithExtraPrizeModifier(10)
                    .Build(),
            },

            JokerIdentifier.BaseVogal => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'A', 'E', 'I', 'O', 'U'})
                    .WithExtraPrizeModifier(5)
                    .Build(),
            },

            JokerIdentifier.MultM => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'M'})
                    .WithExtraMultiplierModifier(4)
                    .Build(),
            },

            JokerIdentifier.MultiplyZ => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'Z'})
                    .WithMultiplyMultiplierModifier(2)
                    .Build(),
            },

            JokerIdentifier.MoneyS => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'S'})
                    .WithMoneyModifier(Player, 1)
                    .Build(),
            },

            JokerIdentifier.PowerUpBombB => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'B'})
                    .WithPowerUpModifier(Player, PowerUpType.Bomba, 1)
                    .Build(),
            },

            JokerIdentifier.MultSimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithExtraMultiplierModifier(10)
                    .Build(),
            },

            JokerIdentifier.MultiplySimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithMultiplyMultiplierModifier(2)
                    .Build(),
            },

            JokerIdentifier.EscalatingJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithScoreCounterModifier(Player, id)
                    .WithExtraMultiplierFromCounterModifier(Player, id)
                    .Build(),
                new OnLetterScoredListenerBuilder()
                    .WithScoreCounterModifier(Player, id)
                    .WithExtraMultiplierFromCounterModifier(Player, id)
                    .Build(),
            },

            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}

public enum JokerIdentifier
{
    BaseA,
    BaseVogal,
    MultM,
    MultiplyZ,
    MoneyS,
    PowerUpBombB,
    MultSimpleWord,
    MultiplySimpleWord,
    EscalatingJoker
}