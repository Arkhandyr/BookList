using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookList.Model
{
    public class Badge
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
