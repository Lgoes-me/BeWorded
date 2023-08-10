using System;

public class Cell<T> where T : ICellData
{
    public int I { get; }
    public int J { get; }
    public T Data { get; private set; }
    public bool Empty { get; private set; }
    
    public Cell(int i, int j, Func<T> cellDataCreator)
    {
        I = i;
        J = j;
        Data = cellDataCreator();
    }

    public void ChangeData(T newData, bool empty)
    {
        Data = newData;
        Empty = empty;
    }
    
    public void EmptyCell(bool empty)
    {
        Empty = empty;
    }
}

public interface ICellData
{
    void Deactivate();
    void ResetData();

    int GetSiblingIndex();
    void SetSiblingIndex(int index);
}