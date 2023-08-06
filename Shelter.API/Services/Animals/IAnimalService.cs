using Shelter.Common.Dtos;

namespace Shelter.API.Services.Animals
{
    public interface IAnimalService
    {
        void AddAnimal(AnimalDto dto);
        void RemoveAnimal(AnimalDto dto);
        void UpdateAnimal(AnimalDto dto);
        AnimalDto GetAnimalById(int id);
        List<AnimalDto> GetAllAnimals();
    }
}
