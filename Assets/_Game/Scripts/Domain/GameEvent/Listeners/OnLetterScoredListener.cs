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
        if (Validators.Any(validator => !validator.Validate(letter)))
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

    public OnLetterScoredListenerBuilder WithValidator(LetterValidator validator)
    {
        Validators.Add(validator);
        return this;
    }

    public OnLetterScoredListenerBuilder WithModifier(ScoreModifier modifier)
    {
        Modifiers.Add(modifier);
        return this;
    }
    
    public OnLetterScoredListener Build()
    {
        return new OnLetterScoredListener(Validators, Modifiers);
    }
}