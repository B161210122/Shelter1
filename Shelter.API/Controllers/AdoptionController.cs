using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Services.Adoptions;
using Shelter.Common.Dtos;

namespace Shelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AdoptionController : ControllerBase
    {
        private readonly IAdoptionService _adoptionService;

        public AdoptionController(IAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AdoptionDto result = _adoptionService.GetAdoptionById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<AdoptionDto> result = _adoptionService.GetAdoptionList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AdoptionDto adoption)
        {
            _adoptionService.AddAdoption(adoption);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(AdoptionDto adoption)
        {
            _adoptionService.UpdateAdoption(adoption);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(AdoptionDto adoption)
        {
            _adoptionService.RemoveAdoption(adoption);
            return Ok();
        }
    }
}
