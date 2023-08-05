using Shelter.Common.JWT;
using Shelter.Domain.Entities;

namespace Shelter.Common.Dtos
{
    public class RefreshedTokensDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
