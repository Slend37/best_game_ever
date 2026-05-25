using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using BestGameEver.Gameplay;
using BestGameEver.Services;

namespace BestGameEver.Core;

public class Level
{
    public IEnumerable<Cell> Cells => cells.Values;
    private int widthMinSize = 5;
    private int heightMinSize = 5;
    private int newAppleChance = 5;
    private int applesSpawned = 0;

    public Level(int width, int height)
    {
        if (width < widthMinSize || height < heightMinSize)
            throw new ArgumentException("The level size is too small. It must be at least 5x5.");
        
        creators = new List<IAppleCreator>
        {
            new GoldenAppleCreator(),
            new NormalAppleCreator()
        };
        

        for (int line = 0; line < height; line++)
        {
            for (int column = 0; column < width; column++)
            {
                if (line == 0 || column == 0 || line == height - 1 || column == width - 1)
                {
                    AddCell(new Cell(new Position(line, column), false));
                }
                else
                {
                    AddCell(CreateInsideCell(line, column));
                }
                    
            }
        }
    }

    private Cell CreateInsideCell(int line, int column)
    {
        Cell result = new Cell(new Position(line, column), true);
        Random random = new Random((int)DateTime.Now.Ticks);
        while(random.Next(100) < newAppleChance)
            result.AddApple(CreateApple());

        return result;
    }

    private IApple CreateApple()
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        applesSpawned += 1;

        return creators[random.Next(creators.Count)].Create();
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
            throw new ArgumentException("GAME OVER!!!");
        return cells[position];
    }
    public bool CanMoveTo(Position position)
    {
        return cells.ContainsKey(position) &&
               cells[position].IsPassable;
    }

    public int GetApplesCount()
    {
        return applesSpawned;
    }
    private readonly Dictionary<Position, Cell> cells = new();
    private readonly List<IAppleCreator> creators;
}