using System.Collections.Generic;
using System.Linq;

public class OnWordScoredJoker : BaseJoker
{
    private List<WordValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    internal OnWordScoredJoker(
        JokerIdentifier id, 
        List<WordValidator> validators, 
        List<ScoreModifier> modifiers) : 
        base(id)
    {
        Validators = validators;
        Modifiers = modifiers;
    }

    public void OnWordScored(ref int basePrice, ref int baseMultiplier, string word)
    {
        if (!Validators.Any(validator => validator.Validate(word)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier.DoModification(ref basePrice, ref baseMultiplier);
        }
    }
}

public class OnWordScoredJokerBuilder
{
    private List<WordValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    public OnWordScoredJokerBuilder()
    {
        Validators = new List<WordValidator>();
        Modifiers = new List<ScoreModifier>();
    }

    public OnWordScoredJokerBuilder WithWordPatternValidator(string subString)
    {
        Validators.Add(new WordPatternValidator(subString));
        return this;
    }
    
    public OnWordScoredJokerBuilder WithNoValidator()
    {
        Validators.Add(new NoValidator());
        return this;
    }

    public OnWordScoredJokerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifiers.Add(new ExtraPrizeScoreModifier(bonus));
        return this;
    }

    public OnWordScoredJokerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifiers.Add(new ExtraMultiplierScoreModifier(bonus));
        return this;
    }

    public OnWordScoredJokerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifiers.Add(new MultiplyMultiplierScoreModifier(bonus));
        return this;
    }

    public OnWordScoredJokerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifiers.Add(new MoneyPerCharacterScoreModifier(player, bonus));
        return this;
    }
    
    public OnWordScoredJokerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifiers.Add(new PowerUpPerScoreModifier(player, powerUpType, bonus));
        return this;
    }
    
    public OnWordScoredJokerBuilder WithScoreCounterModifier(Player player, JokerIdentifier identifier)
    {
        Modifiers.Add(new ScoreCounterModifier(player, identifier));
        return this;
    }

    public OnWordScoredJokerBuilder WithExtraMultiplierFromCounterModifier(Player player, JokerIdentifier identifier)
    {
        Modifiers.Add(new ExtraMultiplierFromCounterScoreModifier(player, identifier));
        return this;
    }

    public OnWordScoredJoker Build(JokerIdentifier id)
    {
        return new OnWordScoredJoker(id, Validators, Modifiers);
    }
}