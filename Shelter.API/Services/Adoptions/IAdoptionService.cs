using Shelter.Common.Dtos;

namespace Shelter.API.Services.Adoptions
{
    public interface IAdoptionService
    {
        void AddAdoption(AdoptionDto adoptionDto);
        void RemoveAdoption(AdoptionDto adoptionDto);
        void UpdateAdoption(AdoptionDto adoptionDto);
        AdoptionDto GetAdoptionById(int id);
        List<AdoptionDto> GetAdoptionList();
    }
}
