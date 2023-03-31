using RaceAndPerformance.Dal.Repository.Contracts;
using RaceAndPerformance.Application.Services.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaceAndPerformance.Application.Mapper;
using RaceAndPerformance.Application.Models.Fetch;
using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Update;

namespace RaceAndPerformance.Application.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly MapperlyMappings _mapperlyMappings;

        public MatchService(IMatchRepository matchRepository, MapperlyMappings mapperlyMappings)
        {
            _matchRepository = matchRepository;
            _mapperlyMappings = mapperlyMappings;
        }

        public async Task<List<GetMatch>> GetAllMatches(CancellationToken cancellationToken)
        {
            var items = await _matchRepository.GetAllAsync(cancellationToken);

            return _mapperlyMappings.MapDtoToGetMatchList(items);
        }

        public async Task<GetMatch> GetMatch(long id, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(id, cancellationToken);

            return _mapperlyMappings.MapDtoToGetMatch(match);
        }

        public async Task<long> InsertMatch(CreateMatch createMatch, CancellationToken cancellationToken)
        {
            var dto = _mapperlyMappings.MapCreateMatchToDto(createMatch);

            return await _matchRepository.InsertAsync(dto, cancellationToken);
        }

        public async Task<long> UpdateMatch(UpdateMatch updateMatch, CancellationToken cancellationToken)
        {
            var dto = _mapperlyMappings.MapUpdateMatchToDto(updateMatch);

            return await _matchRepository.UpdateAsync(dto, cancellationToken);
        }

        public async Task<bool> DeleteMatch(long matchId, CancellationToken cancellationToken)
        {
            return await _matchRepository.DeleteAsync(matchId, cancellationToken);
        }
    }
}