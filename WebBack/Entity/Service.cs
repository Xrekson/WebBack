using WebBack.Database;
using WebBack.Encryption;
using WebBack.Model;

namespace WebBack.Entity
{
    public class Service
    {
        private readonly Dbconnect _context;
        private readonly EncService _encrypt;

        public Service(Dbconnect context,EncService encrypt)
        {
            _context = context;
            _encrypt = encrypt;
        }

        public List<Users> GetCustomers()
        {
            return  _context.Users.AsQueryable().ToList();
        }

        public  Users GetCustomer(string email,string _password)
        {
            var password = _encrypt.Encrypt(_password);
            var Email = email;
            return  _context.Users.Find(Email, password);
        }

        public  Users CreateCustomer(Users customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            customer.password = _encrypt.Encrypt(customer.password);
            _context.Users.Add(customer);
             _context.SaveChanges();
            return customer;
        }

        public  Users UpdateCustomer(string email, Users updatedCustomer)
        {
            var existingCustomer =  GetCustomer(email, updatedCustomer.password);
            if (existingCustomer == null)
            {
                throw new Exception("Customer not found");
            }
            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.password = _encrypt.Encrypt(updatedCustomer.password);
            existingCustomer.Mobile = updatedCustomer.Mobile;
            //existingCustomer. = updatedCustomer.address;
            _context.Users.Update(existingCustomer);
             _context.SaveChanges();
            return existingCustomer;
        }
    }
}