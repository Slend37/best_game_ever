using BestGameEver.Core;

namespace BestGameEver.Tests;

public class SnakeTests
{
    [Fact]
    public void Constructor_WithValidParameters_SetsAllProperties()
    {
        var startPosition = new Position(5, 5);

        var snake = new Snake(3, true, 7, startPosition, 10, Direction.Right);

        Assert.Equal(3, snake.Size);
        Assert.True(snake.Protect);
        Assert.Equal(7, snake.ProtectTime);
        Assert.Equal(5, snake.Position.Line);
        Assert.Equal(5, snake.Position.Column);
        Assert.Equal(10, snake.WinSize);
        Assert.Equal(Direction.Right, snake.Direction);
    }

    [Fact]
    public void Move_Up_ChangesPositionAndDirection()
    {
        var snake = new Snake(3, false, 0, new Position(5, 5), 8, Direction.Right);

        snake.Move(Direction.Up);

        Assert.Equal(4, snake.Position.Line);
        Assert.Equal(5, snake.Position.Column);
        Assert.Equal(Direction.Up, snake.Direction);
    }

    [Fact]
    public void Constructor_SizeLessThanThree_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Snake(2));
    }

    [Fact]
    public void Constructor_WinSizeLessThanSize_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Snake(5, false, 0, new Position(5, 5), 4, Direction.Right));
    }

    [Fact]
    public void Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented()
    {
        var snake = new Snake(3, false, 0, new Position(5, 5), 8, Direction.Right);

        snake.Move(Direction.Left);

        Assert.Equal(Direction.Right, snake.Direction);
        Assert.Equal(5, snake.Position.Line);
        Assert.Equal(6, snake.Position.Column);
    }
}
