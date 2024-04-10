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

    public OnWordScoredListenerBuilder WithValidator(WordValidator validator)
    {
        Validators.Add(validator);
        return this;
    }

    public OnWordScoredListenerBuilder WithModifier(ScoreModifier modifier)
    {
        Modifiers.Add(modifier);
        return this;
    }
    
    public OnWordScoredListener Build()
    {
        return new OnWordScoredListener(Validators, Modifiers);
    }
}