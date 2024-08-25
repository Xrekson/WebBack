using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace WebBack.Model
{

    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
