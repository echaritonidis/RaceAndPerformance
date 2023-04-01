using RaceAndPerformance.Dal.Repository.Contracts;
using RaceAndPerformance.Application.Services.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RaceAndPerformance.Application.Models.Fetch;
using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Update;
using AutoMapper;
using RaceAndPerformance.Core.Dto;

namespace RaceAndPerformance.Application.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMatch>> GetAllMatches(CancellationToken cancellationToken)
        {
            var items = await _matchRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<GetMatch>>(items);
        }

        public async Task<GetMatch> GetMatch(long id, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<GetMatch>(match);
        }

        public async Task<long> InsertMatch(CreateMatch createMatch, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<MatchDto>(createMatch);

            return await _matchRepository.InsertAsync(dto, cancellationToken);
        }

        public async Task<long> UpdateMatch(UpdateMatch updateMatch, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<MatchDto>(updateMatch);

            return await _matchRepository.UpdateAsync(dto, cancellationToken);
        }

        public async Task<bool> DeleteMatch(long matchId, CancellationToken cancellationToken)
        {
            return await _matchRepository.DeleteAsync(matchId, cancellationToken);
        }
    }
}