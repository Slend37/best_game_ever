using System.Dynamic;
using Microsoft.VisualBasic;
using BestGameEver;
using BestGameEver.Core.Components;
using System.IO.Compression;

namespace BestGameEver.Core;

public class Snake
{
    public int Size { get; set; }
    public bool Protect {get; set; }
    public int ProtectTime {get; set; }
    public int WinSize {get; set; }
    public Direction Direction {get; private set; }

    public Body Body {get ; set; }

    public Position Position {get; set; }
    private int minSize = 3;
    private int startWinSize = 10;
    private int startProtectTime = 0;
    private Position startPosition = new Position(5,5);

    private Body body = new Body();

    public Snake(int size)
    {
        if (size < minSize)
            throw new ArgumentException("Snake size must be 3 or more");

        Size = size;
        Protect = false;
        ProtectTime = startProtectTime;
        Position = startPosition;
        WinSize = startWinSize;
        Direction = Direction.Right;
        body.AddPosition(5, 5);
        body.AddPosition(5, 4);
        body.AddPosition(5, 3);
        Body = body;

    }
    public Snake(int size, bool protect, int protectTime, Position position, int winSize, Direction direction, Body body)
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
        Body = body;

    }

    public void Move(Direction direction)
    {
        for (int i = Body.Count - 1; i > 0; i--)
        {
            Body.UpdatePosition(i, Body.GetPositionAt(i-1).Line, Body.GetPositionAt(i-1).Column);
        }
        switch (direction)
        {
            case Direction.Up:
                Position = Position.Up();
                Body.UpdatePosition(0, Body.GetPositionAt(0).Line - 1,  Body.GetPositionAt(0).Column);
                break;
            case Direction.Down:
                Position = Position.Down();
                Body.UpdatePosition(0, Body.GetPositionAt(0).Line + 1,  Body.GetPositionAt(0).Column);
                break;
            case Direction.Left:
                Position = Position.Left();
                Body.UpdatePosition(0, Body.GetPositionAt(0).Line,  Body.GetPositionAt(0).Column - 1);
                break;
            case Direction.Right:
                Position = Position.Right();
                Body.UpdatePosition(0, Body.GetPositionAt(0).Line,  Body.GetPositionAt(0).Column + 1);
                break;
        }
        Direction = direction;
    }
    public void _SetDirection(Direction direction)
    {
        Direction = direction;
    }
}