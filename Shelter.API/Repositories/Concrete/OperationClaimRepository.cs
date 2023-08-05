using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class OperationClaimRepository : Repository.Repository<OperationClaim>, IOperationClaimRepository
    {
        public OperationClaimRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
