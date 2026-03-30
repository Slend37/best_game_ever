namespace BestGameEver.Core
{
    public class SizeBonusDecorator : SnakeStatsDecorator
    {
        private readonly int bonus;
        public SizeBonusDecorator(ISnakeStats wrapped, int bonus) : base(wrapped) => this.bonus = bonus;

        public override int GetSize() => base.GetSize() + bonus;
    }
}