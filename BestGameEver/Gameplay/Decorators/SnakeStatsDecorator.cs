namespace BestGameEver.Core
{
    public abstract class SnakeStatsDecorator : ISnakeStats
    {
        protected readonly ISnakeStats wrapped;
        protected SnakeStatsDecorator(ISnakeStats wrapped) => this.wrapped = wrapped;

        public virtual int GetSize() => wrapped.GetSize();
        public virtual int GetProtectTime() => wrapped.GetProtectTime();
    }
}