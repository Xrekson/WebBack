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
    public class UsersController : ControllerBase
    {
        private readonly Service _service;

        public UsersController(Service service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetCustomers()
        {
            return await _service.GetCustomersAsync();
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<Users>> GetCustomerAsync([FromBody] Users customer)
        {
            if (customer == null)
            {
                return NotFound();
            }
            var Dbcustomer = await _service.GetCustomerAsync(customer.Email, customer.password);
            return Ok(new { Message = "Logged IN!", Id = Dbcustomer.Id, name = Dbcustomer.Name, email = Dbcustomer.Email });
        }

        [HttpPost]
        public async Task<ActionResult<Users>>CreateCustomerAsync(Users customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCustomer = await _service.CreateCustomerAsync(customer);
            Console.WriteLine(createdCustomer);
            return Ok(new { Message = "Customer created successfully", Id = createdCustomer.Id ,name = createdCustomer.Name, email = createdCustomer.Email});
        }
    }
}