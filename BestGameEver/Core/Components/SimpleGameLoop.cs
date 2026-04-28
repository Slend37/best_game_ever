// Core/Components/SimpleGameLoop.cs
using System.Diagnostics;

namespace BestGameEver.Core.Components;

public class SimpleGameLoop : IGameLoop
{
    private readonly Stopwatch _stopwatch;
    private float _targetFrameTime = 1f / 60f;
    
    public int CurrentFrame { get; private set; }
    public float DeltaTime { get; private set; }
    
    public SimpleGameLoop()
    {
        _stopwatch = new Stopwatch();
    }
    
    public void Start()
    {
        _stopwatch.Restart();
        CurrentFrame = 0;
        DeltaTime = 0;
    }
    
    public void Update()
    {
        DeltaTime = (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();
        
        if (DeltaTime < _targetFrameTime)
        {
            Thread.Sleep((int)((_targetFrameTime - DeltaTime) * 1000));
            DeltaTime = _targetFrameTime;
        }
        
        CurrentFrame++;
    }
    
    public void Stop()
    {
        _stopwatch.Stop();
    }
}