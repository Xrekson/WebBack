using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<List<Users>> GetCustomersAsync()
        {
            return await _context.Users.AsQueryable().ToListAsync();
        }

        public async Task<Users> GetCustomerAsync(string email,string _password)
        {
            var password = _encrypt.Encrypt(_password);
            var Email = email;
            return await _context.Users.FindAsync(new object[] { Email, password });
        }

        public async Task<Users> CreateCustomerAsync(Users customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            customer.password = _encrypt.Encrypt(customer.password);
            _context.Users.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}