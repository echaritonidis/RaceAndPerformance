using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RaceAndPerformance.Core.Dto;

namespace RaceAndPerformance.Dal.Repository.Contracts
{
    public interface IMatchRepository
    {
        public Task<List<MatchDto>> GetAllAsync(CancellationToken cancellationToken);

        public Task<MatchDto> GetByIdAsync(long matchId, CancellationToken cancellationToken);

        public Task<long> InsertAsync(MatchDto match, CancellationToken cancellationToken);

        public Task<long> UpdateAsync(MatchDto match, CancellationToken cancellationToken);

        public Task<bool> DeleteAsync(long matchId, CancellationToken cancellationToken);
    }
}
