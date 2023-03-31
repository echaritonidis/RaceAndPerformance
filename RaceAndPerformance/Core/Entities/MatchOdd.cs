namespace RaceAndPerformance.Core.Entities
{
    public class MatchOdd : BaseEntity
    {
        public string Specifier { get; private set; }

        public double Odd { get; private set; }

        public Match Match { get; private set; }

        public long MatchId { get; private set; }

        public MatchOdd(string specifier, double odd) : base()
        {
            this.Specifier = specifier;
            this.Odd = odd;
        }

        public MatchOdd(long id, string specifier, double odd) : base(id)
        {
            this.Specifier = specifier;
            this.Odd = odd;
        }
    }
}