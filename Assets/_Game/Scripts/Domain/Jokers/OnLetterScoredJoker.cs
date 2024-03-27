using System.Collections.Generic;

public class OnLetterScoredJoker : BaseJoker
{
    private LetterValidator Validator { get; set; }
    private ScoreModifier Modifier { get; set; }

    internal OnLetterScoredJoker(JokerIdentifier id, LetterValidator validator, ScoreModifier modifier) :
        base(id)
    {
        Validator = validator;
        Modifier = modifier;
    }

    public void OnLetterScored(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Validator.Validate(letter))
            return;

        Modifier.DoModification(ref basePrice, ref baseMultiplier);
    }
}

public class OnLetterScoredJokerBuilder
{
    private LetterValidator Validator { get; set; }
    private ScoreModifier Modifier { get; set; }

    public OnLetterScoredJokerBuilder WithContainsCharacterValidator(List<char> characters)
    {
        Validator = new ContainsCharacterLetterValidator(characters);
        return this;
    }

    public OnLetterScoredJokerBuilder WithIsPrizeOnValidator(IPrize prize)
    {
        Validator = new IsPrizeLetterValidator(prize);
        return this;
    }

    public OnLetterScoredJokerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifier = new ExtraPrizeScoreModifier(bonus);
        return this;
    }

    public OnLetterScoredJokerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifier = new ExtraMultiplierScoreModifier(bonus);
        return this;
    }

    public OnLetterScoredJokerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifier = new MultiplyMultiplierScoreModifier(bonus);
        return this;
    }

    public OnLetterScoredJokerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifier = new MoneyPerCharacterScoreModifier(player, bonus);
        return this;
    }
    
    public OnLetterScoredJokerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifier = new PowerUpPerScoreModifier(player, powerUpType, bonus);
        return this;
    }

    public OnLetterScoredJoker Build(JokerIdentifier id)
    {
        return new OnLetterScoredJoker(id, Validator, Modifier);
    }
}