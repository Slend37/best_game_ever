using BestGameEver.Gameplay;

namespace BestGameEver.Services;

public class NormalAppleCreator : IAppleCreator
{
    private int addValue = 1;
    public NormalAppleCreator(int value)
    {
        this.value = value;
    }

    public NormalAppleCreator()
    {
        this.value = addValue;
    }
    public IApple Create()
    {
        return new NormalApple(value);
    }
    

    private readonly int value;
}
