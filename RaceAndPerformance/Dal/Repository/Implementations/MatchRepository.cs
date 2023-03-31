using RaceAndPerformance.Dal.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using RaceAndPerformance.Core.Entities;
using System.Linq;
using RaceAndPerformance.Core.Dto;
using System;
using System.Globalization;

namespace RaceAndPerformance.Dal.Repository.Implementations
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IMssqlRepository<Match> _repository;
        
        public MatchRepository(IMssqlRepository<Match> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieve all matches
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<MatchDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllWithRelatedDataAsync(cancellationToken, o => o.MatchOdds);

            return items.Select(x => new MatchDto
            {
                Id = x.Id,
                Description = x.Description,
                MatchDate = x.MatchDate.ToString("dd-MM-yyyy"),
                MatchTime = x.MatchTime.ToString("HH:mm"),
                TeamA = x.TeamA,
                TeamB = x.TeamB,
                Sport = x.Sport,
                MatchOdds = x.MatchOdds.Select(o => new MatchOddDto
                {
                    Id = o.Id,
                    Odd = o.Odd,
                    Specifier = o.Specifier
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// Retrieve single match by Id
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MatchDto> GetByIdAsync(long matchId, CancellationToken cancellationToken)
        {
            var match = await _repository.GetByIdWithRelatedDataAsync(matchId, cancellationToken, o => o.MatchOdds);

            if (match == null) return null;

            return new MatchDto
            {
                Id = match.Id,
                Description = match.Description,
                MatchDate = match.MatchDate.ToString("dd-MM-yyyy"),
                MatchTime = match.MatchTime.ToString("HH:mm"),
                TeamA = match.TeamA,
                TeamB = match.TeamB,
                Sport = match.Sport,
                MatchOdds = match.MatchOdds.Select(o => new MatchOddDto
                {
                    Id = o.Id,
                    Odd = o.Odd,
                    Specifier = o.Specifier
                }).ToList()
            };
        }

        /// <summary>
        /// Create a new Match object
        /// </summary>
        /// <param name="match"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> InsertAsync(MatchDto match, CancellationToken cancellationToken)
        {
            var model = new Match
            (
                match.Description,
                DateTime.ParseExact(match.MatchDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                DateTime.ParseExact(match.MatchTime, "HH:mm", CultureInfo.InvariantCulture),
                match.TeamA,
                match.TeamB,
                match.Sport
            );

            model.MatchOdds.AddRange
            (
                match.MatchOdds?.Select(o => new MatchOdd(o.Specifier, o.Odd)).ToList() ?? new()
            );

            return await _repository.InsertAsync(model, cancellationToken);
        }

        /// <summary>
        /// Modify a Match object
        /// </summary>
        /// <param name="match"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(MatchDto match, CancellationToken cancellationToken)
        {
            var matchToUpdate = await _repository.GetByIdWithRelatedDataAsync(match.Id, cancellationToken, o => o.MatchOdds);

            if (matchToUpdate is null) return 0;

            matchToUpdate.UpdateMatch
            (
                match.Description,
                DateTime.ParseExact(match.MatchDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                DateTime.ParseExact(match.MatchTime, "HH:mm", CultureInfo.InvariantCulture),
                match.TeamA,
                match.TeamB,
                match.Sport
            );

            matchToUpdate.MatchOdds.Clear();
            matchToUpdate.MatchOdds.AddRange
            (
                match.MatchOdds?.Select(o => new MatchOdd(o.Id, o.Specifier, o.Odd)).ToList() ?? new()
            );

            await _repository.UpdateAsync(matchToUpdate, cancellationToken);

            return matchToUpdate.Id;
        }

        /// <summary>
        /// Delete a Match object
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(long matchId, CancellationToken cancellationToken)
        {
            return _repository.DeleteById(matchId, cancellationToken);
        }
    }
}
