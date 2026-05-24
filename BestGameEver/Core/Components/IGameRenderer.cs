namespace BestGameEver.Core.Components;

public interface IGameRenderer
{
    void Clear();
    void Draw(char[,] buffer);
    void DrawStatus(string status);
    void Flush();
    void DrawText(string text);
    void Draw_(char[,] buffer);
}