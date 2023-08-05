using Shelter.API.Repositories.Abstract;
using Shelter.Domain.Context;
using Shelter.Domain.Entities;

namespace Shelter.API.Repositories.Concrete
{
    public class RefreshTokenRepository : Repository.Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
