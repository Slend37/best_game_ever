using Xunit;

namespace BestGameEver.Tests;

public class GameLoopFacadeTests
{
    private class FakeGame : IGame
    {
        public int HandleInputCalls { get; private set; }
        public int UpdateCalls { get; private set; }
        public int RenderCalls { get; private set; }

        private int iterationCount = 0;

        public bool IsGameEnded()
        {
            iterationCount++;
            return iterationCount > 1;
        }

        public void HandleInput()
        {
            HandleInputCalls++;
        }

        public void Update()
        {
            UpdateCalls++;
        }

        public void Render()
        {
            RenderCalls++;
        }
    }

    [Fact]
    public void Run_Should_Call_HandleInput_Update_And_Render_One_Time()
    {
        var fakeGame = new FakeGame();
        var facade = new GameLoopFacade(fakeGame);

        facade.Run();

        Assert.Equal(1, fakeGame.HandleInputCalls);
        Assert.Equal(1, fakeGame.UpdateCalls);
        Assert.Equal(1, fakeGame.RenderCalls);
    }
}