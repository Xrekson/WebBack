using Microsoft.AspNetCore.Mvc;
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
        public List<City> GetCitys()
        {
            return _service.GetCities();
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetCitysById(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dbcity =  _service.GetCity(id);
            return Ok(new { Message = "City Data", data = dbcity });
        }

        [HttpPost]
        public  ActionResult<Users> CreateCustomer(City city)
        {
            if (!ModelState.IsValid || city == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                 _service.CreateCity(city);
                City citydb =  _service.GetCity(city.Id);
                City dbCity = citydb;
                Console.WriteLine(dbCity);
                return Ok(new { Message = "City created successfully", Id = city.Id, name = city.Name, description = city.Description });
            }
        }
    }
}