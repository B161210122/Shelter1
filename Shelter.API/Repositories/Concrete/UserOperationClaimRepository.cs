using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class UserOperationClaimRepository : Repository.Repository<UserOperationClaim>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
