using System.Collections.Generic;

public class OnPowerUpUsedGameEvent : BaseGameEvent<OnPowerUpUsedListener>
{
    public void Invoke(PowerUp shuffle, List<LetterController> letters)
    {
        foreach (var listener in OrderedListeners)
        {
            listener.OnBoardShuffled(shuffle, letters);
        }
    }
}