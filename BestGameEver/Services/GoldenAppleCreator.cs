using BestGameEver.Gameplay;

namespace BestGameEver.Services;

public class GoldenAppleCreator : IAppleCreator
{
    public GoldenAppleCreator(int value, int time)
    {
        this.value = value;
        this.time = time;
    }

    public GoldenAppleCreator()
    {
        this.value = 1;
        this.time = 10;
    }
    public IApple Create()
    {
        return new GoldenApple(value, time);
    }

    private readonly int value;
    private readonly int time;
}
