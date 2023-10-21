using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookList.Model
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Pages { get; set; }
        public string Cover { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public int Year { get; set; }
        public string Publisher { get; set; } = null!;
        public InteractionData InteractionData { get; set; }
    }

    public class InteractionData
    {
        public long Planning { get; set; }
        public long Reading { get; set; }
        public long Done { get; set; }
    }
}