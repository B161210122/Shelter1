using Shelter.Common.Dtos;

namespace Shelter.API.Services.Genusses
{
    public interface IGenusService
    {
        void AddGenus(GenusDto dto);
        void UpdateGenus(GenusDto dto);
        void DeleteGenus(GenusDto dto);
        GenusDto GetGenusById(int id);
        List<GenusDto> GetAll();
    }
}
