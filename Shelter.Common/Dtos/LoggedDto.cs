using Shelter.Common.JWT;
using Shelter.Domain.Entities;

namespace Shelter.Common.Dtos
{
    public class LoggedDto
    {
        public AccessToken? AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public LoggedResponseDto CreateResponseDto()
        {
            return new LoggedResponseDto()
            {
                AccessToken = AccessToken,
                OperationClaimId = OperationClaimId,
                UserId =  UserId
            };
        }

        public class LoggedResponseDto
        {
            public AccessToken? AccessToken { get; set; }
            public int UserId { get; set; }
            public int OperationClaimId { get; set; }
        }
    }
}
