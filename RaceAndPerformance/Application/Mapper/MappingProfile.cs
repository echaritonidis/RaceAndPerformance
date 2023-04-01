using AutoMapper;
using RaceAndPerformance.Application.Models.Create;
using RaceAndPerformance.Application.Models.Fetch;
using RaceAndPerformance.Application.Models.Update;
using RaceAndPerformance.Core.Dto;

namespace RaceAndPerformance.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MatchDto, GetMatch>();
            CreateMap<MatchOddDto, GetMatchOdd>();

            CreateMap<CreateMatch, MatchDto>();
            CreateMap<CreateMatchOdd, MatchOddDto>();

            CreateMap<UpdateMatch, MatchDto>();
            CreateMap<UpdateMatchOdd, MatchOddDto>();
        }
    }
}
