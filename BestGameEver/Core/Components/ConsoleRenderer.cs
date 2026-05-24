using System.Text;

namespace BestGameEver.Core.Components;

public class ConsoleRenderer : IGameRenderer
{
    private readonly int _width;
    private readonly int _height;
    
    public ConsoleRenderer(int width, int height)
    {
        _width = width;
        _height = height;
        Console.CursorVisible = false;
    }
    
    public void Clear()
    {
        Console.Clear();
    }
    
    public void Draw(char[,] buffer)
    {
        var sb = new StringBuilder();
        for (int line = 0; line < _height; line++)
        {
            for (int column = 0; column < _width; column++)
            {
                sb.Append(buffer[line, column]);
            }
            sb.AppendLine();
        }
        Console.Write(sb.ToString());
    }
    
    public void DrawStatus(string status)
    {
        Console.SetCursorPosition(0, _height);
        Console.Write(status.PadRight(Console.WindowWidth - 1));
    }
    
    public void Flush()
    {
    }
    public void DrawText(string text)
    {
        Console.WriteLine(text);
    }
    public void Draw_(char[,] buffer)
    {
        for (int i = 0; i < buffer.GetLength(0); i++)
        {
            for (int j = 0; j < buffer.GetLength(1); j++)
            {
                Console.Write(buffer[i, j]);
            }
            Console.WriteLine();
        }
    }
}