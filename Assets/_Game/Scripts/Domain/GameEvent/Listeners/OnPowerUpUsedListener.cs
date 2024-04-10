using System.Collections.Generic;
using System.Linq;

public class OnPowerUpUsedListener : BaseGameEventListener
{
    private List<PowerUpValidator> Validators { get; set; }
    private List<PowerUpModifier> Modifiers { get; set; }
    
    internal OnPowerUpUsedListener(
        List<PowerUpValidator> validators, 
        List<PowerUpModifier> modifiers)
    {
        Validators = validators;
        Modifiers = modifiers;
    }

    public void OnBoardShuffled(PowerUp powerUp, List<Letter> letters)
    {
        if (Validators.Any(validator => !validator.Validate(powerUp, letters)))
            return;

        foreach (var modifier in Modifiers)
        {
            modifier.DoModification(powerUp, letters);
        }
    }
}

public class OnPowerUpUsedListenerBuilder
{
    private List<PowerUpValidator> Validators { get; set; }
    private List<PowerUpModifier> Modifiers { get; set; }

    public OnPowerUpUsedListenerBuilder()
    {
        Validators = new List<PowerUpValidator>();
        Modifiers = new List<PowerUpModifier>();
    }

    public OnPowerUpUsedListenerBuilder WithValidator(PowerUpValidator validator)
    {
        Validators.Add(validator);
        return this;
    }

    public OnPowerUpUsedListenerBuilder WithModifier(PowerUpModifier modifier)
    {
        Modifiers.Add(modifier);
        return this;
    }
    
    public OnPowerUpUsedListener Build()
    {
        return new OnPowerUpUsedListener(Validators, Modifiers);
    }
}