using AutoMapper;
using Shelter.API.Repositories.Abstract;
using Shelter.Common.Dtos;
using Shelter.Domain.Entities;

namespace Shelter.API.Services.Genusses
{
    public class GenusManager : IGenusService
    {
        private readonly IGenusRepository _genusRepository;
        private readonly IMapper _mapper;

        public GenusManager(IGenusRepository genusRepository, IMapper mapper)
        {
            _genusRepository = genusRepository;
            _mapper = mapper;
        }

        public void AddGenus(GenusDto dto)
        {
            Genus genus = _mapper.Map<Genus>(dto);
            _genusRepository.Add(genus);
            _genusRepository.SaveChanges();
        }

        public void DeleteGenus(GenusDto dto)
        {
            Genus genus = _mapper.Map<Genus>(dto);
            _genusRepository.Delete(genus);
            _genusRepository.SaveChanges();
        }

        public List<GenusDto> GetAll()
        {
            List<GenusDto> result =_mapper.Map<List<GenusDto>>(_genusRepository.GetAll());
            return result;
        }

        public GenusDto GetGenusById(int id)
        {
            return _mapper.Map<GenusDto>(_genusRepository.GetById(id));
        }

        public void UpdateGenus(GenusDto dto)
        {
            _genusRepository.Update(_mapper.Map<Genus>(dto));
            _genusRepository.SaveChanges();
        }
    }
}
