using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Fetch;
using RaceAndPerformance.Application.Models.Update;
using RaceAndPerformance.Core.Dto;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace RaceAndPerformance.Application.Mapper
{
    [Mapper]
    public partial class MapperlyMappings
    {
        public partial GetMatch MapDtoToGetMatch(MatchDto match);
        public partial List<GetMatch> MapDtoToGetMatchList(List<MatchDto> matches);
        public partial MatchDto MapCreateMatchToDto(CreateMatch match);
        public partial MatchDto MapUpdateMatchToDto(UpdateMatch match);
    }
}
