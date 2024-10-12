using WebBack.Database;
using WebBack.Utility;
using WebBack.Model;
using WebBack.Data;
using Microsoft.EntityFrameworkCore;

namespace WebBack.Entity
{
    public class Service
    {
        private readonly Dbconnect _context;
        private readonly EncService _encrypt;

        public Service(Dbconnect context, EncService encrypt)
        {
            _context = context;
            _encrypt = encrypt;
        }

        public List<Users> GetCustomers()
        {
            return _context.Users.AsQueryable().ToList();
        }

        public Users GetCustomer(int id)
        {
            return _context.Users.Find(id);
        }
        public  Users GetCustomer(string email,string _password)
        {
            var password = _encrypt.Encrypt(_password);
            var Email = email;
            return  _context.Users.Find(Email, password);
        }
        public UserData CreateCustomer(UserData customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            customer.password = _encrypt.Encrypt(customer.password);
            var customerDb = new Users();
            customerDb.Name = customer.name;
            customerDb.Email = customer.email;
            customerDb.password = customer.password;
            customerDb.Mobile = customer.mobile;
            _context.Users.Add(customerDb);
            _context.SaveChanges();
            return customer;
        }

        public UserData UpdateCustomer(UserData updatedCustomer)
        {
            try
            {
                var userDB = GetCustomer(updatedCustomer.id);
                if (userDB == null)
                {
                    throw new Exception("Customer not found");
                }
                userDB.Name = updatedCustomer.name;
                userDB.password = _encrypt.Encrypt(updatedCustomer.password);
                userDB.Mobile = updatedCustomer.mobile;
                userDB.Email = updatedCustomer.email;
                //userDB. = updatedCustomer.address;
                _context.Users.Update(userDB);
                _context.SaveChanges();
                return new UserData(userDB.Id, userDB.Name, userDB.Email, userDB.Mobile);
            }catch(DbUpdateException error){
                throw new ApiError(error.GetBaseException().Message,error.Message);
            }
        }
    }
}