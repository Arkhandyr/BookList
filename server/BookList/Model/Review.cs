using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookList.Model
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public ObjectId User_id { get; set; }
        public ObjectId Book_id { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public List<string> Likes { get; set; }

        public Review()
        {
            _id = ObjectId.GenerateNewId().ToString();
        }
    }
}
