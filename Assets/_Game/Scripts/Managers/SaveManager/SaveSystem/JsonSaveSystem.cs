using System.IO;
using Newtonsoft.Json;

public class JsonSaveSystem<T> : SaveSystem<T> where T : class
{
    public override void SaveFile(string path, T state)
    {
        using (var stream = File.Open(path, FileMode.Create))
        using (var streamWriter = new StreamWriter(stream))
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, state);
        }
    }

    public override T LoadFile(string path)
    {        
        if (!File.Exists(path))
            return null;

        using (var stream = File.Open(path, FileMode.Open))
        using (var streamReader = new StreamReader(stream))
        using (var jsonStream = new JsonTextReader(streamReader))
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(jsonStream);
        }
    }
}