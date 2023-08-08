using Microsoft.AspNetCore.Mvc;
using Shelter.Common.Dtos;
using Shelter.Domain.Entities;
using Shelter.MVC.Provider;

namespace Shelter.MVC.Controllers
{
    public class GenusController : BaseController
    {
        GenussesProvider _genussesProvider;

        public GenusController(GenussesProvider genussesProvider)
        {
            _genussesProvider = genussesProvider;
        }

        public async Task<IActionResult> Index()
        {
            var genusses = await _genussesProvider.GetAll(await this.getAccessTokenFromSession());
            ViewBag.Genusses = genusses;
            var operationClaimId = (HttpContext.Session.GetString("operationClaimId"));
            ViewBag.OperationClaimId = operationClaimId == null ? 0 : Int32.Parse(operationClaimId);
            return View();
        }

        public async Task<IActionResult> AddOrUpdateGenus(int genusId)
        {
            if (genusId < 1) return View(new GenusDto());
            var genus = await _genussesProvider.GetGenusById(genusId, await this.getAccessTokenFromSession());
            ViewBag.Genusses = await _genussesProvider.GetAll(await this.getAccessTokenFromSession());
            return View(genus);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAnimal(GenusDto dto)
        {
            if (dto.Id > 0)
            {
                await _genussesProvider.UpdateGenus(dto, await this.getAccessTokenFromSession());
            }
            else
            {
                await _genussesProvider.AddGenus(dto, await this.getAccessTokenFromSession());
            }
            return Redirect("/genus/index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnimal(int genusId)
        {
            await _genussesProvider.DeleteGenus(new GenusDto() { Id = genusId }, await this.getAccessTokenFromSession());
            return Redirect("/genus/index");
        }
    }
}
