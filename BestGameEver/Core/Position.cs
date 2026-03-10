using System.Dynamic;

namespace BestGameEver.Core;

public struct Position
{
    public int Line {get; private set; }
    public int Column {get; private set; }

    public Position(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public Position Up()
    {
        return new Position(Line - 1, Column);
    }
    public Position Down()
    {
        return new Position(Line + 1, Column);
    }
    public Position Right()
    {
        return new Position(Line, Column + 1);
    }
    public Position Left()
    {
        return new Position(Line, Column - 1);
    }
}