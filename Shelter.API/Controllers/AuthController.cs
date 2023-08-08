using Microsoft.AspNetCore.Mvc;
using Shelter.API.Repositories.Abstract;
using Shelter.API.Services.Auth;
using Shelter.Common.Dtos;
using Shelter.Common.Extensions;
using Shelter.Common.Hashing;
using Shelter.Common.JWT;
using Shelter.Domain.Entities;

namespace Shelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public AuthController(IUserRepository userRepository, IAuthService authService, IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
        {
            User? user = _userRepository.Get(x => x.Email == userForLoginDto.Email);
            user.UserOperationClaims = _userOperationClaimRepository.GetAll(x=>x.UserId == user.Id);

            if (user == null) return Ok(null);

            LoggedDto loggedDto = new();

            AccessToken createdAccessToken = _authService.CreateAccessToken(user);

            RefreshToken createdRefreshToken = _authService.CreateRefreshToken(user, getIpAddress());
            RefreshToken addedRefreshToken = _authService.AddRefreshToken(createdRefreshToken);
            _authService.DeleteOldRefreshTokens(user.Id);

            loggedDto.AccessToken = createdAccessToken;
            loggedDto.RefreshToken = addedRefreshToken;
            loggedDto.UserId = user.Id;
            loggedDto.OperationClaimId = user.UserOperationClaims.Select(x=>x.OperationClaimId).FirstOrDefault();

            setRefreshTokenToCookie(loggedDto.RefreshToken);

            return Ok(loggedDto.CreateResponseDto());
        }

        [HttpPost("Register")]
        public IActionResult Register(UserForRegisterDto dto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
            User newUser = new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            User createdUser = _userRepository.Add(newUser);
            _userRepository.SaveChanges();

            _userOperationClaimRepository.Add(new UserOperationClaim(0, createdUser.Id, 2));
            _userOperationClaimRepository.SaveChanges();

            AccessToken createdAccessToken = _authService.CreateAccessToken(createdUser);

            RefreshToken createdRefreshToken = _authService.CreateRefreshToken(createdUser, getIpAddress());
            RefreshToken addedRefreshToken = _authService.AddRefreshToken(createdRefreshToken);

            RegisteredDto registeredDto = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            setRefreshTokenToCookie(registeredDto.RefreshToken);

            return Created("", registeredDto);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken()
        {
            RefreshToken? refreshToken = _authService.GetRefreshTokenByToken(getRefreshTokenFromCookies());

            if (refreshToken.Revoked != null)
                _authService.RevokeDescendantRefreshTokens(refreshToken, getIpAddress(),
                   $"Attempted reuse of revoked ancestor token : {refreshToken.Token}");

            User user = _userRepository.GetById(refreshToken.Id);

            RefreshToken newRefreshToken = _authService.RotateRefreshToken(user, refreshToken, getIpAddress());
            RefreshToken addedRefreshToken = _authService.AddRefreshToken(newRefreshToken);

            _authService.DeleteOldRefreshTokens(refreshToken.UserId);

            AccessToken createdAccessToken = _authService.CreateAccessToken(user);

            RefreshedTokensDto refreshedTokensDto = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };

            setRefreshTokenToCookie(refreshedTokensDto.RefreshToken);

            return Created("", refreshedTokensDto.AccessToken);
        }

        [HttpPut("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Allow)] string? refreshToken)
        {
            if (refreshToken == null)
            {
                refreshToken = getRefreshTokenFromCookies();
            }
            RefreshToken? Token = _authService.GetRefreshTokenByToken(refreshToken ?? getRefreshTokenFromCookies());


            _authService.RevokeRefreshToken(Token, getIpAddress(), "Revoked without replacement");

            RevokedTokenDto revokedTokenDto = new RevokedTokenDto() { Id = Token.Id, Token = Token.Token };

            return Ok(revokedTokenDto);
        }

        private void setRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
        private string getRefreshTokenFromCookies()
        {
            return Request.Cookies["refreshToken"];
        }
        private string? getIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

        private int getUserIdFromRequest()
        {
            int userId = HttpContext.User.GetUserId();
            return userId;
        }
    }
}
