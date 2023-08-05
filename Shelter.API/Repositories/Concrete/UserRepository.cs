using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class UserRepository : Repository.Repository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
