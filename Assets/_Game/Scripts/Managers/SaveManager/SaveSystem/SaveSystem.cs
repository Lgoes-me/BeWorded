public abstract class SaveSystem<T> where T : class
{
    public abstract void SaveFile(string path, T state);
    public abstract T LoadFile(string path);
}