using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBack.Data;
using WebBack.Utility;
using WebBack.Entity;

namespace WebBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Service _service;
        private AuthService _authservice;

        public UsersController(Service service)
        {
            _service = service;
            _authservice = new AuthService();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  ActionResult<List<UserData>> GetCustomers()
        {
            var customers = _service.GetCustomers();
            var customersOut = customers.Select(x => new UserData(x.Id,x.Name,x.Email,x.Mobile)).ToList();
            if(customers is not null){
                return Ok(customersOut);
            }else{
                return new List<UserData>();
            }
        }

        [HttpPost]
        [Route("authenticate")]
        public  ActionResult<UserData> GetJWT([FromBody] Auth customer)
        {
            if (customer.Email == null || customer.Password == null)
            {
                return NotFound();
            }
            var Dbcustomer =  _service.GetCustomer(customer.Email, customer.Password);
            if (Dbcustomer == null)
            {
                return NotFound();
            }
            var tokenAuth =  _authservice.GenerateToken(Dbcustomer);
            return Ok(new{ Message = "Logged IN!",token = tokenAuth.token, expires = tokenAuth.expiresIn});
        }

        [HttpPost]
        [Route("register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  ActionResult<UserData> CreateCustomer([FromBody]UserData customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCustomer =  _service.CreateCustomer(customer);
            Console.WriteLine(createdCustomer);
            return Ok(new { Message = "Customer created successfully", Id = createdCustomer.id ,name = createdCustomer.name, email = createdCustomer.email});
        }

        [HttpPut]
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public  ActionResult<UserData> UpdateCustomer([FromBody]UserData customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCustomer =  _service.UpdateCustomer(customer);
            Console.WriteLine(createdCustomer);
            return Ok(new { Message = "Customer created successfully", Id = createdCustomer.id ,name = createdCustomer.name, email = createdCustomer.email});
        }
    }
}