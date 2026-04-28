using BestGameEver.Gameplay;

namespace BestGameEver.Services;

public class GoldenAppleCreator : IAppleCreator
{
    private int addValue = 1;
    private int addTime = 10;
    public GoldenAppleCreator(int value, int time)
    {
        this.value = value;
        this.time = time;
    }

    public GoldenAppleCreator()
    {
        this.value = addValue;
        this.time = addTime;
    }
    public IApple Create()
    {
        return new GoldenApple(value, time);
    }

    private readonly int value;
    private readonly int time;
}
