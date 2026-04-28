namespace BestGameEver.Core.Components;
public interface IGameLoop
{
    void Start();
    void Update();
    void Stop();
    int CurrentFrame { get; }
    float DeltaTime { get; }
}