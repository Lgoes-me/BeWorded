using System.Collections.Generic;
using System.Linq;

public class OnWordScoredListener : BaseGameEventListener
{
    private List<WordValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    internal OnWordScoredListener(
        List<WordValidator> validators, 
        List<ScoreModifier> modifiers)
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

public class OnWordScoredListenerBuilder
{
    private List<WordValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    public OnWordScoredListenerBuilder()
    {
        Validators = new List<WordValidator>();
        Modifiers = new List<ScoreModifier>();
    }

    public OnWordScoredListenerBuilder WithWordPatternValidator(string subString)
    {
        Validators.Add(new WordPatternValidator(subString));
        return this;
    }
    
    public OnWordScoredListenerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifiers.Add(new ExtraPrizeScoreModifier(bonus));
        return this;
    }

    public OnWordScoredListenerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifiers.Add(new ExtraMultiplierScoreModifier(bonus));
        return this;
    }

    public OnWordScoredListenerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifiers.Add(new MultiplyMultiplierScoreModifier(bonus));
        return this;
    }

    public OnWordScoredListenerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifiers.Add(new MoneyPerCharacterScoreModifier(player, bonus));
        return this;
    }
    
    public OnWordScoredListenerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifiers.Add(new PowerUpPerScoreModifier(player, powerUpType, bonus));
        return this;
    }
    
    public OnWordScoredListenerBuilder WithScoreCounterModifier(Player player, JokerIdentifier identifier)
    {
        Modifiers.Add(new ScoreCounterModifier(player, identifier));
        return this;
    }

    public OnWordScoredListenerBuilder WithExtraMultiplierFromCounterModifier(Player player, JokerIdentifier identifier)
    {
        Modifiers.Add(new ExtraMultiplierFromCounterScoreModifier(player, identifier));
        return this;
    }

    public OnWordScoredListener Build()
    {
        return new OnWordScoredListener(Validators, Modifiers);
    }
}