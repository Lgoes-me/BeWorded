using System.Collections.Generic;
using System.Linq;

public class OnLetterScoredJoker : BaseJoker
{
    private List<LetterValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    internal OnLetterScoredJoker(
        JokerIdentifier id, 
        List<LetterValidator> validators, 
        List<ScoreModifier> modifiers) :
        base(id)
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

public class OnLetterScoredJokerBuilder
{
    private List<LetterValidator> Validators { get; set; }
    private List<ScoreModifier> Modifiers { get; set; }

    public OnLetterScoredJokerBuilder()
    {
        Validators = new List<LetterValidator>();
        Modifiers = new List<ScoreModifier>();
    }

    public OnLetterScoredJokerBuilder WithContainsCharacterValidator(List<char> characters)
    {
        Validators.Add(new ContainsCharacterLetterValidator(characters));
        return this;
    }

    public OnLetterScoredJokerBuilder WithIsPrizeOnValidator(IPrize prize)
    {
        Validators.Add(new IsPrizeLetterValidator(prize));
        return this;
    }

    public OnLetterScoredJokerBuilder WithExtraPrizeModifier(int bonus)
    {
        Modifiers.Add(new ExtraPrizeScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredJokerBuilder WithExtraMultiplierModifier(int bonus)
    {
        Modifiers.Add(new ExtraMultiplierScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredJokerBuilder WithMultiplyMultiplierModifier(int bonus)
    {
        Modifiers.Add(new MultiplyMultiplierScoreModifier(bonus));
        return this;
    }

    public OnLetterScoredJokerBuilder WithMoneyModifier(Player player, int bonus)
    {
        Modifiers.Add(new MoneyPerCharacterScoreModifier(player, bonus));
        return this;
    }
    
    public OnLetterScoredJokerBuilder WithPowerUpModifier(Player player, PowerUpType powerUpType, int bonus)
    {
        Modifiers.Add(new PowerUpPerScoreModifier(player, powerUpType, bonus));
        return this;
    }

    public OnLetterScoredJoker Build(JokerIdentifier id)
    {
        return new OnLetterScoredJoker(id, Validators, Modifiers);
    }
}