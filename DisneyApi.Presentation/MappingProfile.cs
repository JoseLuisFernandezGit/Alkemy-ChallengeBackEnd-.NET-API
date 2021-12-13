using AutoMapper;
using DisneyApi.Domain.Dtos;
using DisneyApi.Domain.Entities;

namespace DisneyApi.Presentation
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Character, CharacterDto>();
            CreateMap<CharacterDtoForCreationOrUpdate, Character>();
            CreateMap<Movie, MovieDto>()
            .ForMember(PeliculaDto => PeliculaDto.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("yyyy-MM-dd")));
            CreateMap<Character, CharacterDtoForDetails>();
            CreateMap<MovieDtoForCreationOrUpdate, Movie>();
            CreateMap<Movie, MovieDtoForDetails>()
            .ForMember(PeliculaDtoForDetails => PeliculaDtoForDetails.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("yyyy-MM-dd"))); ;
        }
    }
}
