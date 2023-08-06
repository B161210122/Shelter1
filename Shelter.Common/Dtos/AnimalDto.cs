using Shelter.Domain.Entities.Enums;

namespace Shelter.Common.Dtos
{
    public class AnimalDto : BaseDto
    {
        public int GenusId { get; set; }
        public string Genus { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public string Description { get; set; }
        public DateTime AcceptionDate { get; set; }
    }
}
