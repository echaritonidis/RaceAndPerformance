using System.Collections.Generic;
using RaceAndPerformance.Core.Enum;

namespace RaceAndPerformance.Application.Models.Create
{
    public class CreateMatch
    {
        public string Description { get; set; }

        public string MatchDate { get; set; }

        public string MatchTime { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public SportType Sport { get; set; }

        public List<CreateMatchOdd> MatchOdds { get; set; }
    }
}