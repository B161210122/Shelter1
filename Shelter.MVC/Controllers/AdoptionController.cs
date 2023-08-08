using Microsoft.AspNetCore.Mvc;
using Shelter.Common.Dtos;
using Shelter.MVC.Provider;

namespace Shelter.MVC.Controllers
{
    public class AdoptionController : BaseController
    {
        AdoptionProvider _adoptionProvider;
        AnimalProvider _animalProvider;

        public AdoptionController(AdoptionProvider adoptionProvider, AnimalProvider animalProvider)
        {
            _adoptionProvider = adoptionProvider;
            _animalProvider = animalProvider;
        }

        public async Task<ActionResult> Index()
        {
            var userId = Int32.Parse(HttpContext.Session.GetString("userId"));
            var token =await this.getAccessTokenFromSession() ?? "";
            var adoptions =await _adoptionProvider.GetAll(token);
            var animals = await _animalProvider.GetAnimals(token);

            var result = animals.Where(x=>adoptions.Where(x=>x.UserId == userId).Select(x=>x.AnimalId).Contains(x.Id)).Select(animal => new AnimalDto()
            {
                Id = animal.Id,
                Name = animal.Name,
                AcceptionDate = animal.AcceptionDate,
                Age = animal.Age,
                CreatedDate = animal.CreatedDate,
                Description = animal.Description,
                Gender = animal.Gender, 
                Genus = animal.Genus,
                GenusId = animal.GenusId,
                UpdatedDate = animal.UpdatedDate

            }).ToList();

            ViewBag.Animals = result;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAdoption(int adoptionId)
        {
            await _adoptionProvider.DeleteAdoption(new AdoptionDto() { Id = adoptionId }, await this.getAccessTokenFromSession());
            return Redirect("/Adoption/Index");
        }
    }
}
