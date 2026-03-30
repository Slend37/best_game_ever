namespace BestGameEver.Core
{
    public class SizePenaltyDecorator : SnakeStatsDecorator
    {
        private readonly int penalty;
        public SizePenaltyDecorator(ISnakeStats wrapped, int penalty) : base(wrapped)
        {
            this.penalty = penalty;
        }

        public override int GetSize()
        {
            int baseSize = base.GetSize();
            int result = baseSize - penalty;
            return result < 1 ? 1 : result;
        }
    }
}