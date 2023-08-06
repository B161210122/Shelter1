using Shelter.Domain.Entities.Enums;

namespace Shelter.Common.Dtos
{
    public class AdoptionDto : BaseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public StatusEnum Status { get; set; }
    }
}
