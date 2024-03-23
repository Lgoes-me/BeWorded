public interface ISavable<T>
{
    string Id { get; }
    T SaveData();
}
