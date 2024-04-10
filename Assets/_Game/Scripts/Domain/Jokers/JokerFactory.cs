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
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'A'}))
                    .WithModifier(new ExtraPrizeScoreModifier(10))
                    .Build(),
            },

            JokerIdentifier.BaseVogal => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'A', 'E', 'I', 'O', 'U'}))
                    .WithModifier(new ExtraPrizeScoreModifier(5))
                    .Build(),
            },

            JokerIdentifier.MultM => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'M'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build(),
            },

            JokerIdentifier.MultiplyZ => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'Z'}))
                    .WithModifier(new MultiplyMultiplierScoreModifier(2))
                    .Build(),
            },

            JokerIdentifier.MoneyS => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'S'}))
                    .WithModifier(new MoneyPerCharacterScoreModifier(Player, 1))
                    .Build(),
            },

            JokerIdentifier.PowerUpBombB => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'B'}))
                    .WithModifier(new PowerUpPerScoreModifier(Player, PowerUpType.Bomba, 1))
                    .Build(),
            },

            JokerIdentifier.MultSimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new ExtraMultiplierScoreModifier(10))
                    .Build(),
            },

            JokerIdentifier.MultiplySimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new MultiplyMultiplierScoreModifier(2))
                    .Build(),
            },

            JokerIdentifier.EscalatingJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new ScoreCounterModifier(Player, id))
                    .Build(),
                new OnLetterScoredListenerBuilder()
                    .WithModifier(new ExtraMultiplierFromCounterScoreModifier(Player, id))
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