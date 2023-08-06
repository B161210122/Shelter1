using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class AdoptionRepository : Repository.Repository<Adoption>, IAdoptionRepository
    {
        public AdoptionRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}