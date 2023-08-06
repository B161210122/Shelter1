using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Repositories.Abstract;
using Shelter.API.Services.Animals;
using Shelter.Common.Dtos;

namespace Shelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AnimalDto result = _animalService.GetAnimalById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<AnimalDto> result = _animalService.GetAllAnimals();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AnimalDto animal)
        {
            _animalService.AddAnimal(animal);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(AnimalDto animal)
        {
            _animalService.UpdateAnimal(animal);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(AnimalDto animal)
        {
            _animalService.RemoveAnimal(animal);
            return Ok();
        }
    }
}
