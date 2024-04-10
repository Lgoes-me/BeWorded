using System.Collections.Generic;
using System.Linq;

public class OnLetterScoredListener : BaseGameEventListener
{
    private List<LetterValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    internal OnLetterScoredListener(
        List<LetterValidator> validators, 
        List<ScoreModifier> modifiers)
    {
        Validators = validators;
        Modifiers = modifiers;
    }

    public void OnLetterScored(ref int basePrice, ref int baseMultiplier, Letter letter)
    {
        if (!Validators.Any(validator => validator.Validate(letter)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier.DoModification(ref basePrice, ref baseMultiplier);
        }
    }
}

public class OnLetterScoredListenerBuilder
{
    private List<LetterValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    public OnLetterScoredListenerBuilder()
    {
        Validators = new List<LetterValidator>();
        Modifiers = new List<ScoreModifier>();
    }

    public OnLetterScoredListenerBuilder WithContainsCharacterValidator(List<char> characters)
    {
        Validators.Add(new ContainsCharacterLetterValidator(characters));
        return this;
    }

    public OnLetterScoredListenerBuilder WithIsPrizeOnValidator(IPrize prize)
    {
        Validators.Add(new IsPrizeLetterValidator(prize));
        return this;
    }

    public OnLetterScoredListenerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifiers.Add(new ExtraPrizeScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredListenerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifiers.Add(new ExtraMultiplierScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredListenerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifiers.Add(new MultiplyMultiplierScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredListenerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifiers.Add(new MoneyPerCharacterScoreModifier(player, bonus));
        return this;
    }
    
    public OnLetterScoredListenerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifiers.Add(new PowerUpPerScoreModifier(player, powerUpType, bonus));
        return this;
    }

    public OnLetterScoredListener Build()
    {
        return new OnLetterScoredListener(Validators, Modifiers);
    }
}