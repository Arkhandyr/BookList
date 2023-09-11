using MongoDB.Bson;

namespace BookList.Model
{
    public class LikeEntry
    {
        public string Username { get; set; }
        public string ReviewId { get; set; }
    }
}
