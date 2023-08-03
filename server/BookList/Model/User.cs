using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookList.Model
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Username { get; set; } = null!;
        public string RealName { get; set; } = null!;
        [JsonIgnore]
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Picture { get; set; }
        public string Bio { get; set; }
    }
}
