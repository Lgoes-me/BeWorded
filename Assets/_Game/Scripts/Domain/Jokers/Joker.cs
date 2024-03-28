public abstract class BaseJoker : ISavable<JokerModel>
{
    public string Id => Identifier.ToString();
    private JokerIdentifier Identifier { get; }

    protected BaseJoker(JokerIdentifier identifier)
    {
        Identifier = identifier;
    }

    public JokerModel SaveData()
    {
        return new JokerModel(Id);
    }
}

//OnPowerUpUsed
///OnLetterSwaped
///OnBombExploded
///OnBoardShuffled
//OnMatchEnded
//OnItemBought
