using Shelter.Common.JWT;
using Shelter.Domain.Entities;

namespace Shelter.API.Services.Auth
{
    public interface IAuthService
    {
        public AccessToken CreateAccessToken(User user);
        public RefreshToken CreateRefreshToken(User user, string ipAddress);
        public RefreshToken GetRefreshTokenByToken(string token);
        public RefreshToken AddRefreshToken(RefreshToken refreshToken);
        public void DeleteOldRefreshTokens(int userId);
        public void RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason);

        public void RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null,
                                       string? replacedByToken = null);

        public RefreshToken RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress);
    }
}
