using AutoMapper;
using Shelter.Common.Dtos;
using Shelter.Domain.Entities;

namespace Shelter.API.Profiles
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<GenusDto, Genus>().ReverseMap();
            CreateMap<Animal, AnimalDto>().ForMember(x => x.Genus, x => x.MapFrom(x => x.Genus.Name))
                .ReverseMap();
            CreateMap<Adoption, AdoptionDto>()
                .ForMember(x => x.UserName, x => x.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
                .ForMember(x => x.AnimalName, x => x.MapFrom(x => x.Animal.Name))
                .ReverseMap();
        }
    }
}
