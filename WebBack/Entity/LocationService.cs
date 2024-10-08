using WebBack.Model;
using Microsoft.EntityFrameworkCore;
using WebBack.Database;
using WebBack.Encryption;
namespace WebBack.Entity
{

    public class LocationService
    {
        private readonly Dbconnect _context;
        private readonly EncService _encrypt;

        public LocationService(Dbconnect context, EncService encrypt)
        {
            _context = context;
            _encrypt = encrypt;
        }

        public List<City> GetCities()
        {
            return _context.Cites.AsQueryable().ToList();
        }

        public City GetCity(string id)
        {
            return _context.Cites.Find(id);
        }
        public List<Country> GetCountries()
        {
            return _context.Countries.AsQueryable().ToList();
        }

        public Country GetCountry(string id)
        {
            return _context.Countries.Find(id);
        }

        public City CreateCity(City city)
        {
            _context.Cites.Add(city);
            _context.SaveChanges();
            return city;
        }

        public Country CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return country;
        }

        public City UpdateCity(City city)
        {
            var existingCity = GetCity(city.Id);
            if (existingCity == null)
            {
                throw new Exception("City not found");
            }
            existingCity.Name = city.Name;
            existingCity.Description = city.Description;
            _context.Cites.Update(existingCity);
            _context.SaveChanges();
            return existingCity;
        }

        public Country UpdateCountry(Country country)
        {
            var existingCountry = GetCountry(country.Id);
            if (existingCountry == null)
            {
                throw new Exception("Country not found");
            }
            existingCountry.Name = country.Name;
            existingCountry.Cities = country.Cities;
            _context.Countries.Update(existingCountry);
            _context.SaveChanges();
            return existingCountry;
        }

        public Boolean DeleteCity(string id)
        {
            var Dcity = _context.Cites.Find(id);
            if (Dcity != null)
            {
                _context.Cites.Remove(Dcity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Boolean DeleteCountry(string id)
        {
            var Dcountry = _context.Countries.Find(id);
            if (Dcountry != null)
            {
                _context.Countries.Remove(Dcountry);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
