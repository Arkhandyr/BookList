using MongoDB.Bson.Serialization.Attributes;

namespace BookList.Model
{
    public class Book
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public Guid _id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Pages { get; set; }
        public string Cover { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public int Year { get; set; }
        public string Publisher { get; set; } = null!;
    }
}