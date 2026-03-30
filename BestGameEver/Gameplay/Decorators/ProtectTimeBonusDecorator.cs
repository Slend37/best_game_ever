namespace BestGameEver.Core
{
    public class ProtectTimeBonusDecorator : SnakeStatsDecorator
    {
        private readonly int bonus;
        public ProtectTimeBonusDecorator(ISnakeStats wrapped, int bonus) : base(wrapped) => this.bonus = bonus;

        public override int GetProtectTime() => base.GetProtectTime() + bonus;
    }
}