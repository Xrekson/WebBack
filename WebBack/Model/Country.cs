using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebBack.Model
{
    public class Country
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Code")]
        public string Code { get; set; }

        [BsonElement("Cities")]
        public List<City> Cities { get; set; }
    }
}
