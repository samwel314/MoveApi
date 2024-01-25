using AutoMapper;
using DeVCreed.Data.Models;
using DeVCreed.Dtos;

namespace DeVCreed.Helpers
{
     public class MappingProfile : Profile
     {
        public MappingProfile()
        {
               CreateMap<Movie, MovieDetailsDto>();
        }

     }
}
