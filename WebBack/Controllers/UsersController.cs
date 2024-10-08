using Microsoft.AspNetCore.Mvc;
using WebBack.Entity;
using WebBack.Model;

namespace WebBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Service _service;

        public UsersController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public  ActionResult<List<Users>> GetCustomers()
        {
            return _service.GetCustomers();
        }

        [HttpPost]
        [Route("/login")]
        public  ActionResult<Users> GetCustomer([FromBody] Users customer)
        {
            if (customer == null)
            {
                return NotFound();
            }
            var Dbcustomer =  _service.GetCustomer(customer.Email, customer.password);
            return Ok(new { Message = "Logged IN!", Id = Dbcustomer.Id, name = Dbcustomer.Name, email = Dbcustomer.Email });
        }

        [HttpPost]
        public  ActionResult<Users> CreateCustomer(Users customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCustomer =  _service.CreateCustomer(customer);
            Console.WriteLine(createdCustomer);
            return Ok(new { Message = "Customer created successfully", Id = createdCustomer.Id ,name = createdCustomer.Name, email = createdCustomer.Email});
        }
    }
}