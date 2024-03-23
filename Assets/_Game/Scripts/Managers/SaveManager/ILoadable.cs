public interface ILoadable<T>
{
    string Id { get; set; }
    void LoadData(T data);
}