using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shelter.API.Services.Genusses;
using Shelter.Common.Dtos;

namespace Shelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenusController : ControllerBase
    {
        private readonly IGenusService _genusService;

        public GenusController(IGenusService genusService)
        {
            _genusService = genusService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GenusDto result = _genusService.GetGenusById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GenusDto> result = _genusService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(GenusDto genus)
        {
            _genusService.AddGenus(genus);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(GenusDto genus)
        {
            _genusService.UpdateGenus(genus);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(GenusDto genus)
        {
            _genusService.DeleteGenus(genus);
            return Ok();
        }
    }
}
