using System.Collections.Generic;
using RaceAndPerformance.Core.Enum;

namespace RaceAndPerformance.Application.Models.Update
{
    public class UpdateMatch
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string MatchDate { get; set; }

        public string MatchTime { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public SportType Sport { get; set; }

        public List<UpdateMatchOdd> MatchOdds { get; set; }
    }
}