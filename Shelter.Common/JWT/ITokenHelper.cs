using Shelter.Domain.Entities;

namespace Shelter.Common.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

        RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}
