using Shelter.Domain.Entities.Enums;

namespace Shelter.Domain.Entities
{
    public class Adoption : Entity
    {
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public StatusEnum Status { get; set; }

        public User User { get; set; }
        public Animal Animal { get; set; }

        public Adoption()
        {
        }

        public Adoption(int userId, int animalId, StatusEnum status)
        {
            UserId = userId;
            AnimalId = animalId;
            Status = status;
        }
    }
}
