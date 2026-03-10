using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using BestGameEver.Gameplay;

namespace BestGameEver.Core;

public class Level
{
    public IEnumerable<Cell> Cells => cells.Values;

    public Level(int width, int height)
    {
        if (width < 5 || height < 5)
            throw new ArgumentException("The level size is too small. It must be at least 5x5.");

        for (int line = 0; line < height; ++line)
        {
            for (int column = 0; column < width; ++width)
            {
                if (line == 0 || column == 0 || line == height - 1 || column == width -1)
                    AddCell(new Cell(new Position(line, column), false));
                else
                    AddCell(CreateInsideCell(line, column));
            }
        }
    }

    private static Cell CreateInsideCell(int line, int column)
    {
        Cell result = new Cell(new Position(line, column), true);
        Random random = new Random((int)DateTime.Now.Ticks);
        while(random.Next(100) < 5)
            result.AddApple(CreateApple());
            
        return result;
    }

    private static IApple CreateApple()
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        switch(random.Next(2))
        {
            case 0:
                return new NormalApple(1);
            default:
                return new GoldenApple(1, 10);
        }
    }
    public void AddCell(Cell cell)
    {
        if (!cells.ContainsKey(cell.Position))
            cells.Add(cell.Position, cell);
        else
            cells[cell.Position] = cell;
    }
    public Cell GetCell(Position position)
    {
        if (!cells.ContainsKey(position))
            throw new ArgumentException("This cell does not exist.");
        return cells[position];
    }
    public bool CanMoveTo(Position position)
    {
        return cells.ContainsKey(position) &&
               cells[position].IsPassable;
    }
    private readonly Dictionary<Position, Cell> cells = new();
}