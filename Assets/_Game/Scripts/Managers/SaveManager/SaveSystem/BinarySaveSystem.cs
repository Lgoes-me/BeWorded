using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinarySaveSystem<T> : SaveSystem<T> where T : class
{
    public override void SaveFile(string path, T state)
    {
        using (var stream = File.Open(path, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    public override T LoadFile(string path)
    {
        if (!File.Exists(path))
            return null;

        using (var stream = File.Open(path, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as T;
        }
    }
}