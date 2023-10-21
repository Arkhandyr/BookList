using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BookList.Model
{
    public class Users_Badges
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
        public ObjectId User_id { get; set; }
        public ObjectId Badge_id { get; set; }
        public DateTime Date { get; set; }
    }
}
