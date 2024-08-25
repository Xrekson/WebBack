using MongoDB.Driver;
using WebBack.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebBack.Database;
using MongoDB.Driver.Linq;
namespace WebBack.Entity
{

    public class LocationService
    {
        private readonly IMongoCollection<City> _citiesCollection;
        private readonly IMongoCollection<Country> _countriesCollection;

        public LocationService(IOptions<MongoDbConect> mongoDatabaseSettings)
        {
            var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            _citiesCollection = database.GetCollection<City>("Cities");
            _countriesCollection = database.GetCollection<Country>("Countries");
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await _citiesCollection.Find(x => true).ToListAsync();
        }

        public async Task<City> GetCityAsync(string id)
        {
            return await _citiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<City> GetCityAsyncByAttr(string data, string attr)
        {
            return await _citiesCollection.Find(Builders<City>.Filter.Eq(attr, data)).FirstOrDefaultAsync();
        }
        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _countriesCollection.Find(x => true).ToListAsync();
        }

        public async Task<Country> GetCountryAsync(string id)
        {
            return await _countriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateCityAsync(City city)
        {
            await _citiesCollection.InsertOneAsync(city);
        }

        public async Task CreateCountryAsync(Country country)
        {
            await _countriesCollection.InsertOneAsync(country);
        }

        public async Task UpdateCityAsync(City city)
        {
            await _citiesCollection.ReplaceOneAsync(x => x.Id == city.Id, city);
        }

        public async Task UpdateCountryAsync(Country country)
        {
            await _countriesCollection.ReplaceOneAsync(x => x.Id == country.Id, country);
        }

        public async Task DeleteCityAsync(string id)
        {
            await _citiesCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteCountryAsync(string id)
        {
            await _countriesCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
