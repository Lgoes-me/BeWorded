public abstract class BaseJoker : ISavable<JokerModel>
{
    public string Id { get; private set; }

    protected BaseJoker(string id)
    {
        Id = id;
    }

    public JokerModel SaveData()
    {
        return new JokerModel(Id);
    }
}