using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelter.Common.Dtos;
using Shelter.MVC.Provider;

namespace Shelter.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly AnimalProvider _animal;
        private readonly GenussesProvider _menusses;

        public HomeController(AnimalProvider animal, GenussesProvider menusses)
        {
            _animal = animal;
            _menusses = menusses;
        }

        public async Task<IActionResult> Index()
        {
            var animals = await _animal.GetAnimals(await this.getAccessTokenFromSession());
            ViewBag.Animals = animals;
            var operationClaimId =(HttpContext.Session.GetString("operationClaimId"));
            ViewBag.OperationClaimId = operationClaimId == null ? 0 : Int32.Parse(operationClaimId);
            return View();
        }

        public async Task<IActionResult> AddOrUpdateAnimal(int animalId)
        {
            if (animalId < 1) return View(new AnimalDto());
            var animal = await _animal.GetAnimalById(animalId, await this.getAccessTokenFromSession());
            ViewBag.Genusses = await _menusses.GetAll(await this.getAccessTokenFromSession());
            return View(animal);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateAnimal(AnimalDto dto)
        {
            if(dto.Id > 0)
            {
                await _animal.UpdateAnimal(dto, await this.getAccessTokenFromSession());
            }
            else
            {
                await _animal.AddAnimal(dto, await this.getAccessTokenFromSession());
            }
            return Redirect("/home/index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnimal(int animalId)
        {
            await _animal.DeleteAnimal(new AnimalDto() { Id = animalId },await this.getAccessTokenFromSession());
            return Redirect("/home/index");
        }
    }
}
