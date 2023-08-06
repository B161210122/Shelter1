using AutoMapper;
using Shelter.API.Repositories.Abstract;
using Shelter.Common.Dtos;
using Shelter.Domain.Entities;

namespace Shelter.API.Services.Adoptions
{
    public class AdoptionManager : IAdoptionService
    {
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdoptionManager(IAdoptionRepository adoptionRepository, IAnimalRepository animalRepository, IUserRepository userRepository, IMapper mapper)
        {
            _adoptionRepository = adoptionRepository;
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddAdoption(AdoptionDto adoptionDto)
        {
            _adoptionRepository.Add(_mapper.Map<Adoption>(adoptionDto));
            _adoptionRepository.SaveChanges();
        }

        public AdoptionDto GetAdoptionById(int id)
        {
            var adoption = _adoptionRepository.GetById(id);
            adoption.Animal = _animalRepository.GetById(adoption.AnimalId);
            adoption.User = _userRepository.GetById(adoption.UserId);

            return _mapper.Map<AdoptionDto>(adoption);
        }

        public List<AdoptionDto> GetAdoptionList()
        {
            var adoptions = _adoptionRepository.GetAll();
            var animals = _animalRepository.GetAll();
            var users = _userRepository.GetAll();
            foreach (var adoption in adoptions)
            {
                adoption.Animal = animals.FirstOrDefault(x => x.Id == adoption.AnimalId);
                adoption.User = users.FirstOrDefault(x => x.Id == adoption.UserId);
            }

            return _mapper.Map<List<AdoptionDto>>(adoptions);
        }

        public void RemoveAdoption(AdoptionDto adoptionDto)
        {
            _adoptionRepository.Delete(_mapper.Map<Adoption>(adoptionDto));
            _adoptionRepository.SaveChanges();
        }

        public void UpdateAdoption(AdoptionDto adoptionDto)
        {
            _adoptionRepository.Update(_mapper.Map<Adoption>(adoptionDto));
            _adoptionRepository.SaveChanges();
        }
    }
}
