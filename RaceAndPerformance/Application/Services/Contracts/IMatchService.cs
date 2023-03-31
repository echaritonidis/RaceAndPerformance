using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RaceAndPerformance.Application.Models.Fetch;
using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Update;

namespace RaceAndPerformance.Application.Services.Contracts
{
    public interface IMatchService
    {
        public Task<List<GetMatch>> GetAllMatches(CancellationToken cancellationToken);
        public Task<GetMatch> GetMatch(long id, CancellationToken cancellationToken);
        public Task<long> InsertMatch(CreateMatch createMatch, CancellationToken cancellationToken);
        public Task<long> UpdateMatch(UpdateMatch updateMatch, CancellationToken cancellationToken);
        public Task<bool> DeleteMatch(long matchId, CancellationToken cancellationToken);
    }
}