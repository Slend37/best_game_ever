using BestGameEver.Core;

namespace BestGameEver.Builders;

public class SnakeBuilder
{
    private int size = 3;
    private bool protect = false;
    private int protectTime = 0;
    private Position position = new Position(5, 5);
    private int winSize = 8;
    private Direction direction = Direction.Right;
    private Body body = new Body();

    public SnakeBuilder SetSize(int value)
    {
        size = value;
        return this;
    }

    public SnakeBuilder SetProtection(bool value)
    {
        protect = value;
        return this;
    }

    public SnakeBuilder SetProtectionTime(int value)
    {
        protectTime = value;
        return this;
    }

    public SnakeBuilder SetPosition(Position value)
    {
        position = value;
        return this;
    }

    public SnakeBuilder SetWinSize(int value)
    {
        winSize = value;
        return this;
    }

    public SnakeBuilder SetDirection(Direction value)
    {
        direction = value;
        return this;
    }

    public SnakeBuilder SetBody()
    {
        body.AddPosition(5, 5);
        body.AddPosition(5, 4);
        body.AddPosition(5, 3);
        return this;
    }

    public Snake Build()
    {
        return new Snake(size, protect, protectTime, position, winSize, direction, body);
    }
}