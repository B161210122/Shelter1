using Microsoft.AspNetCore.Mvc;
using Shelter.Common.Dtos;
using Shelter.MVC.Provider;

namespace Shelter.MVC.Controllers
{
    public class AuthController : BaseController
    {
        AuthProvider _auth;

        public AuthController(AuthProvider auth, GenussesProvider menusses)
        {
            _auth = auth;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto dto)
        {
            var resut = await _auth.Login(dto);
            if(resut != null)
            {
                this.setAccessTokenToSession(resut.AccessToken);
                this.UserIdAndOperationClaimIdSet(resut.UserId, resut.OperationClaimId);
                return Redirect("/home/index");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto dto)
        {
            var result = await _auth.Register(dto);

            if(result != null)
            {
                this.setAccessTokenToSession(result.AccessToken);
                return Redirect("/home/index");
            }

            return View();
        }

    }
}
