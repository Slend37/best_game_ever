using BestGameEver.Gameplay;

namespace BestGameEver.Services;

public class NormalAppleCreator : IAppleCreator
{
    public NormalAppleCreator(int value)
    {
        this.value = value;
    }
    public IApple Create()
    {
        return new NormalApple(value);
    }

    private readonly int value;
}
