using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using WebBack.Entity;
using WebBack.Model;

namespace WebBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCitys()
        {
            return await _service.GetCitiesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCitysById(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dbcity = await _service.GetCityAsync(id);
            return Ok(new { Message = "City Data", data = dbcity });
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateCustomerAsync(City city)
        {
            if (!ModelState.IsValid || city == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _service.CreateCityAsync(city);
                City citydb = await _service.GetCityAsyncByAttr(city.Name, "Name");
                City dbCity = citydb;
                Console.WriteLine(dbCity);
                return Ok(new { Message = "City created successfully", Id = city.Id, name = city.Name, description = city.Description });
            }
        }
    }
}