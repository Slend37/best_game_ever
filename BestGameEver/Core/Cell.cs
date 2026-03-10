using BestGameEver.Gameplay;
using BestGameEver.Core;

public class Cell
{
    public Position Position {get; private set; }
    public bool IsPassable{get; private set; }

    public IEnumerable<IApple> Apples => apples;

    public Cell(Position position, bool isPassable)
    {
        Position = position;
        IsPassable = isPassable;
    }

    public void AddApple(IApple apple)
    {
        apples.Add(apple);
    }

    public void RemoveApple(IApple apple)
    {
        apples.Remove(apple);
    }

    public void RemoveAllApples()
    {
        apples.Clear();
    }

    private readonly List<IApple> apples = new();
}