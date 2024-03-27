public class OnWordScoredJoker : BaseJoker
{
    private WordValidator Validator { get; set; }
    private ScoreModifier Modifier { get; set; }

    internal OnWordScoredJoker
        (JokerIdentifier id, WordValidator validator, ScoreModifier modifier) : base(id)
    {
        Validator = validator;
        Modifier = modifier;
    }

    public void OnWordScored(ref int basePrice, ref int baseMultiplier, string word)
    {
        if (!Validator.Validate(word))
            return;

        Modifier.DoModification(ref basePrice, ref baseMultiplier);
    }
}

public class OnWordScoredJokerBuilder
{
    private WordValidator Validator { get; set; }
    private ScoreModifier Modifier { get; set; }

    public OnWordScoredJokerBuilder WithWordPatternValidator(string subString)
    {
        Validator = new WordPatternValidator(subString);
        return this;
    }
    
    public OnWordScoredJokerBuilder WithNoValidator()
    {
        Validator = new NoValidator();
        return this;
    }

    public OnWordScoredJokerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifier = new ExtraPrizeScoreModifier(bonus);
        return this;
    }

    public OnWordScoredJokerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifier = new ExtraMultiplierScoreModifier(bonus);
        return this;
    }

    public OnWordScoredJokerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifier = new MultiplyMultiplierScoreModifier(bonus);
        return this;
    }

    public OnWordScoredJokerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifier = new MoneyPerCharacterScoreModifier(player, bonus);
        return this;
    }
    
    public OnWordScoredJokerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifier = new PowerUpPerScoreModifier(player, powerUpType, bonus);
        return this;
    }

    public OnWordScoredJoker Build(JokerIdentifier id)
    {
        return new OnWordScoredJoker(id, Validator, Modifier);
    }
}