using Shelter.API.Repositories.Abstract;
using Shelter.API.Repositories.Concrete;
using Shelter.Common.JWT;
using Shelter.Domain.Entities;

namespace Shelter.API.Services.Auth
{
    public class AuthManager : IAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly TokenOptions _tokenOptions;
        private readonly IOperationClaimRepository _operationClaimRepository;


        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository,
                           IRefreshTokenRepository refreshTokenRepository,
                           ITokenHelper tokenHelper,
                           IConfiguration configuration,
                           IOperationClaimRepository operationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenHelper = tokenHelper;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _operationClaimRepository = operationClaimRepository;
        }

        public RefreshToken AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken =_refreshTokenRepository.Add(refreshToken);

            return addedRefreshToken;
        }

        
        public  AccessToken CreateAccessToken(User user)
        {
            IList<UserOperationClaim> userOperationClaims = _userOperationClaimRepository.GetAll(p => p.UserId == user.Id);
            IList<OperationClaim> operationClaims = _operationClaimRepository.GetAll(x => userOperationClaims.Select(x => x.OperationClaimId).Contains(x.Id));

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);

            return accessToken;
        }


        public RefreshToken CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);

            return refreshToken;
        }

        public void DeleteOldRefreshTokens(int userId)
        {
            IList<RefreshToken> refreshTokens = (_refreshTokenRepository.GetAll(r =>
                                                    r.UserId == userId &&
                                                    r.Revoked == null && r.Expires >= DateTime.UtcNow &&
                                                    r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <= DateTime.UtcNow)
                                                );

            foreach (RefreshToken refreshToken in refreshTokens) _refreshTokenRepository.Delete(refreshToken);
        }

        public  RefreshToken GetRefreshTokenByToken(string token)
        {
            RefreshToken? refreshToken = _refreshTokenRepository.Get(r => r.Token == token);
            return refreshToken;
        }

        public void RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
        {
            RefreshToken childToken = _refreshTokenRepository.Get(r => r.Token == refreshToken.ReplacedByToken);

            if (childToken != null && childToken.Revoked != null && childToken.Expires <= DateTime.UtcNow)
                RevokeRefreshToken(childToken, ipAddress, reason);
            else RevokeDescendantRefreshTokens(childToken, ipAddress, reason);
        }

        public void RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
            _refreshTokenRepository.Update(token);
        }

        public RefreshToken RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
        {
            RefreshToken newRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);

            return newRefreshToken;
        }

       
    }
}

