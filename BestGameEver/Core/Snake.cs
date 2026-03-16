using System.Dynamic;

namespace BestGameEver.Core;

public class Snake
{
    public int Size { get; set; }
    public bool Protect {get; set; }
    public int ProtectTime {get; set; }
    public int WinSize {get; set; }
    public Direction Direction {get; private set; }

    public Position Position {get; set; }

    public Snake(int size)
    {
        if (size < 3)
            throw new ArgumentException("Snake size must be 3 or more");

        Size = size;
        Protect = false;
        ProtectTime = 0;
        Position = new Position(5, 5);
        WinSize = 8;
        Direction = Direction.Right;

    }
    public Snake(int size, bool protect, int protectTime, Position position, int winSize, Direction direction)
    {
        if (size < 3)
            throw new ArgumentException("Snake size must be 3 or more");

        if (winSize < size)
            throw new ArgumentException("Win size must be greater than or equal to snake size");

        if (protectTime < 0)
            throw new ArgumentException("Protect time can not be negative");


        Size = size;
        Protect = protect;
        ProtectTime += protectTime;
        Position = position;
        WinSize = winSize;
        Direction = direction;

    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                Position = Position.Up();
                break;
            case Direction.Down:
                Position = Position.Down();
                break;
            case Direction.Left:
                Position = Position.Left();
                break;
            case Direction.Right:
                Position = Position.Right();
                break;
        }
        Direction = direction;
    }
}