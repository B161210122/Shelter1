using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class GenusRepository : Repository.Repository<Genus>, IGenusRepository
    {
        public GenusRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
