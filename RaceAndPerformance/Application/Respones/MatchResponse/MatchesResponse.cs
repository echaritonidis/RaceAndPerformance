using RaceAndPerformance.Application.Models.Fetch;
using System.Collections.Generic;

namespace RaceAndPerformance.Application.Respones.MatchResponse
{
    public class MatchesResponse
    {
        public List<GetMatch> Matches { get; set; }
    }
}