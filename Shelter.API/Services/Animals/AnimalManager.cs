using AutoMapper;
using Shelter.API.Repositories.Abstract;
using Shelter.Common.Dtos;
using Shelter.Domain.Entities;

namespace Shelter.API.Services.Animals
{
    public class AnimalManager : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;
        private readonly IGenusRepository _genusRepository;

        public AnimalManager(IAnimalRepository animalRepository, IMapper mapper, IGenusRepository genusRepository)
        {
            _animalRepository = animalRepository;
            _mapper = mapper;
            _genusRepository = genusRepository;
        }

        public void AddAnimal(AnimalDto dto)
        {
            _animalRepository.Add(_mapper.Map<Animal>(dto));
            _animalRepository.SaveChanges();
        }

        public List<AnimalDto> GetAllAnimals()
        {
            var animals = _animalRepository.GetAll();
            var genusses = _genusRepository.GetAll();
            foreach (var animal in animals)
            {
                var temp = genusses.FirstOrDefault(x => x.Id == animal.Id);
                animal.Genus = temp;
            }
            return _mapper.Map<List<AnimalDto>>(animals);
        }

        public AnimalDto GetAnimalById(int id)
        {
            var animal = _animalRepository.GetById(id);
            animal.Genus = _genusRepository.GetById(animal.GenusId);
            return _mapper.Map<AnimalDto>(animal);
        }

        public void RemoveAnimal(AnimalDto dto)
        {
            _animalRepository.Delete(_mapper.Map<Animal>(dto));
            _animalRepository.SaveChanges();
        }

        public void UpdateAnimal(AnimalDto dto)
        {
            _animalRepository.Update(_mapper.Map<Animal>(dto));
            _animalRepository.SaveChanges();
        }
    }
}
