using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookList.Model
{
    public class Review
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
        public ObjectId User_id { get; set; }
        public ObjectId Book_id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
