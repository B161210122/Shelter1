using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class AnimalRepository : Repository.Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
