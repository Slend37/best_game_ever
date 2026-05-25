using System.Dynamic;
using Microsoft.VisualBasic;

namespace BestGameEver.Core;

public class Body
{
    private List<Position> _positions;
    private int blockField1 = 1;
    private int blockField2 = 38;
    private int blockField3 = 1;
    private int blockField4 = 13;

    public Body()
    {
        _positions = new List<Position>();
    }

    public Body(Position[] initialPositions)
    {
        _positions = initialPositions != null 
            ? new List<Position>(initialPositions) 
            : new List<Position>();
    }

    public bool IsHeadCollidingWithBody()
    {
        if (_positions.Count <= 1) return false;
        
        Position head = _positions[0];
        for (int i = 1; i < _positions.Count; i++)
        {
            if (_positions[i].Equals(head))
                return true;
        }

        if ((head.Column < blockField1) || (head.Column > blockField2) ||
            (head.Line < blockField3) || (head.Line > blockField4))
        {
            return true;
        }
        return false;
    }

    public Position[] GetAllPositions()
    {
        return _positions.ToArray();
    }

    public int Count => _positions.Count;

    public bool AddPosition(Position position)
    {

        if (_positions.Contains(position))
            return false;

        _positions.Add(position);
        return true;
    }

    public bool AddPosition(int x, int y)
    {
        return AddPosition(new Position(x, y));
    }

    public bool RemovePosition(Position position)
    {

        return _positions.Remove(position);
    }

    public bool RemovePosition(int x, int y)
    {
        return RemovePosition(new Position(x, y));
    }

    public bool RemovePositionAt(int index)
    {
        if (index < 0 || index >= _positions.Count)
            return false;

        _positions.RemoveAt(index);
        return true;
    }

    public bool UpdatePosition(int index, Position newPosition)
    {

        if (index < 0 || index >= _positions.Count)
            return false;

        _positions[index] = newPosition;
        return true;
    }


    public bool UpdatePosition(int index, int newX, int newY)
    {
        return UpdatePosition(index, new Position(newX, newY));
    }

    public bool ReplacePosition(Position oldPosition, Position newPosition)
    {
        int index = _positions.FindIndex(p => p.Equals(oldPosition));
        if (index == -1)
            return false;

        _positions[index] = newPosition;
        return true;
    }
    public Position GetPositionAt(int index)
    {
        if (index < 0 || index >= _positions.Count)
            throw new IndexOutOfRangeException($"Индекс {index} вне диапазона");

        return _positions[index];
    }

    public bool ContainsPosition(Position position)
    {
        return _positions.Skip(0).Contains(position);
    }

    public void ClearAllPositions()
    {
        _positions.Clear();
    }

    public void PrintAllPositions()
    {
        Console.WriteLine($"Всего позиций: {Count}");
        for (int i = 0; i < _positions.Count; i++)
        {
            Console.WriteLine($"[{i}] {_positions[i]}");
        }
    }

    public Position this[int index]
    {
        get => GetPositionAt(index);
        set => UpdatePosition(index, value);
    }
}