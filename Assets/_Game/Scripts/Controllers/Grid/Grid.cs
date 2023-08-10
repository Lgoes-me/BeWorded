using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid<T> : IEnumerable<Cell<T>> where T : ICellData
{
    private int Height { get; }
    private int Width { get; }

    private Func<T> CellDataCreator { get; }

    private Cell<T>[,] Cells { get; }
    private Dictionary<T, Cell<T>> DataToCell { get; }

    public Grid(int height, int width, Func<T> cellDataCreator)
    {
        Height = height;
        Width = width;
        CellDataCreator = cellDataCreator;

        Cells = new Cell<T>[Height, Width];
        DataToCell = new Dictionary<T, Cell<T>>();

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Cells[i, j] = new Cell<T>(i, j, cellDataCreator);
                DataToCell.Add(Cells[i, j].Data, Cells[i, j]);
            }
        }
    }

    public T Get(int i, int j)
    {
        return Cells[i, j].Data;
    }

    public bool AreNeighbours(T data, T otherData)
    {
        return GetCellsAroundNoDiagonals(data).Contains(otherData);
    }

    private List<T> GetCellsAroundNoDiagonals(T data)
    {
        var cell = DataToCell[data];

        var iMin = Mathf.Clamp(cell.I - 1, 0, Height - 1);
        var iMax = Mathf.Clamp(cell.I + 1, 0, Height - 1);

        var jMin = Mathf.Clamp(cell.J - 1, 0, Width - 1);
        var jMax = Mathf.Clamp(cell.J + 1, 0, Width - 1);

        var cellsAround = new List<Cell<T>>();

        for (var i = iMin; i <= iMax; i++)
        {
            if (i == cell.I) continue;
            cellsAround.Add(Cells[i, cell.J]);
        }

        for (var j = jMin; j <= jMax; j++)
        {
            if (j == cell.J) continue;
            cellsAround.Add(Cells[cell.I, j]);
        }

        return cellsAround.Select(c => c.Data).ToList();
    }

    private Cell<T> GetCellAbove(Cell<T> cell)
    {
        while (cell.Empty)
        {
            var iAbove = Mathf.Clamp(cell.I - 1, 0, Height - 1);
            cell = Cells[iAbove, cell.J];

            if (cell.I == 0)
                break;
        }

        return cell;
    }

    public void ClearCells(List<T> selectedData)
    {
        var cells = selectedData.Select(d => DataToCell[d]).ToList();

        foreach (var cell in cells)
        {
            cell.Data.Deactivate();
            cell.EmptyCell(true);
        }
    }

    public IEnumerator SortEmpty()
    {
        for (var i = Height - 1; i >= 0; i--)
        for (var j = Width - 1; j >= 0; j--)
        {
            var cell = Cells[i, j];
            var cellAbove = GetCellAbove(cell);

            if (cell.Empty)
            {
                SwapCells(cell, cellAbove);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void SwapCells(T data, T otherData)
    {
        var cell = DataToCell[data];
        var otherCell = DataToCell[otherData];

        SwapCells(cell, otherCell);
    }

    public void FillNewData()
    {
        for (var i = Height - 1; i >= 0; i--)
        for (var j = Width - 1; j >= 0; j--)
        {
            var cell = Cells[i, j];

            if (cell.Empty)
            {
                cell.Data.ResetData();
                cell.EmptyCell(false);
                cell.Data.AnimateFall();
            }
        }
    }

    private void SwapCells(Cell<T> cell, Cell<T> otherCell)
    {
        var data = cell.Data;
        var otherData = otherCell.Data;

        var cellSiblingIndex = data.GetSiblingIndex();
        var otherCellSiblingIndex = otherData.GetSiblingIndex();

        data.SetSiblingIndex(otherCellSiblingIndex);
        otherData.SetSiblingIndex(cellSiblingIndex);

        var empty = cell.Empty;
        var otherEmpty = otherCell.Empty;

        cell.ChangeData(otherData, otherEmpty);
        otherCell.ChangeData(data, empty);

        DataToCell[cell.Data] = cell;
        DataToCell[otherCell.Data] = otherCell;
    }

    public IEnumerator<Cell<T>> GetEnumerator()
    {
        foreach (var cell in Cells)
        {
            yield return cell;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}