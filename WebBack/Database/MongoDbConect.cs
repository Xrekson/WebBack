
using MongoDB.Driver;

namespace WebBack.Database
{
    public class MongoDbConect
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

    }
}
