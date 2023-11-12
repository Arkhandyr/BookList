using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookList.Model
{
    public class Follow
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
        public ObjectId User_id { get; set; }
        public ObjectId User2_id { get; set; }
        public DateTime Date { get; set; }
    }
}
