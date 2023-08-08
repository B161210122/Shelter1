using Microsoft.AspNetCore.Mvc;
using Shelter.Common.JWT;
using Shelter.MVC.Provider;

namespace Shelter.MVC.Controllers
{
    public class BaseController : Controller
    {
        private AuthProvider? _auth;
        protected AuthProvider? AuthProvider =>  _auth ??= HttpContext.RequestServices.GetService<AuthProvider>();
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public void setAccessTokenToSession(AccessToken accessToken)
        {
            HttpContext.Session.SetString("accessToken", accessToken.Token);
        }

        public async Task<string> getAccessTokenFromSession()
        {
            var token = HttpContext.Session.GetString("accessToken") ?? "";
            if (token == null)
            {
                var refreshedToken = await _auth.RefreshToken();
                if (refreshedToken == null) token = "";
                else
                {
                    setAccessTokenToSession(refreshedToken);
                    token = refreshedToken.Token;
                }
                
            }
            return token;
        }

        public async Task RevokeToken()
        {
            await _auth.RevokeToken();
        }

        public void UserIdAndOperationClaimIdSet(int userId, int operationClaimId)
        {
            this.UserId = userId;
            this.OperationClaimId = operationClaimId;
            HttpContext.Session.SetString("userId", userId.ToString());
            HttpContext.Session.SetString("operationClaimId", operationClaimId.ToString());
        }
    }
}
