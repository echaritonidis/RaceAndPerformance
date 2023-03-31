using RaceAndPerformance.Core.Enum;
using System;
using System.Collections.Generic;

namespace RaceAndPerformance.Core.Entities
{
    public class Match : BaseEntity
    {
        public string Description { get; private set; }

        public DateTime MatchDate { get; private set; }

        public DateTime MatchTime { get; private set; }

        public string TeamA { get; private set; }

        public string TeamB { get; private set; }

        public SportType Sport { get; private set; }

        public virtual List<MatchOdd> MatchOdds { get; private set; }

        public Match(string description, DateTime matchDate, DateTime matchTime, string teamA, string teamB, SportType sport)
        {
            this.Description = description;
            this.MatchDate = matchDate;
            this.MatchTime = matchTime;
            this.TeamA = teamA;
            this.TeamB = teamB;
            this.Sport = sport;
            this.MatchOdds = new List<MatchOdd>();
        }

        public void UpdateMatch(string description, DateTime matchDate, DateTime matchTime, string teamA, string teamB, SportType sport)
        {
            Description = description;
            MatchDate = matchDate;
            MatchTime = matchTime;
            TeamA = teamA;
            TeamB = teamB;
            Sport = sport;
        }
    }
}