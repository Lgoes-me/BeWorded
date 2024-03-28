using System;
using System.Collections.Generic;

public class JokerFactory
{
    private Player Player { get; }

    public JokerFactory(Player player)
    {
        Player = player;
    }

    public BaseJoker CreateJoker(JokerIdentifier id)
    {
        return id switch
        {
            JokerIdentifier.BaseA =>
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'A'})
                    .WithExtraPrizeModifier(10)
                    .Build(id),

            JokerIdentifier.BaseVogal =>
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'A', 'E', 'I', 'O', 'U'})
                    .WithExtraPrizeModifier(5)
                    .Build(id),

            JokerIdentifier.MultM =>
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'M'})
                    .WithExtraMultiplierModifier(4)
                    .Build(id),

            JokerIdentifier.MultiplyZ => 
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'Z'})
                    .WithMultiplyMultiplierModifier(2)
                    .Build(id),

            JokerIdentifier.MoneyS => 
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'S'})
                    .WithMoneyModifier(Player, 1)
                    .Build(id),

            JokerIdentifier.PowerUpBombB => 
                new OnLetterScoredJokerBuilder()
                    .WithContainsCharacterValidator(new List<char> {'B'})
                    .WithPowerUpModifier(Player, PowerUpType.Bomba, 1)
                    .Build(id),
            
            JokerIdentifier.MultSimpleWord => 
                new OnWordScoredJokerBuilder()
                    .WithNoValidator()
                    .WithExtraMultiplierModifier(10)
                    .Build(id),

            JokerIdentifier.MultiplySimpleWord => 
                new OnWordScoredJokerBuilder()
                    .WithNoValidator()
                    .WithMultiplyMultiplierModifier(2)
                    .Build(id),


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
}