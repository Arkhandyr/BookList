using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.BadgeRepository
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly MongoDbContext context;

        public BadgeRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public List<Badge> GetUserBadges(string username)
        {
            User user = context.Users.Find(u => u.Username == username).FirstOrDefault();

            List<Users_Badges> userBadges = context.Users_Badges.Find(u => u.User_id == user._id).ToList();

            List<Badge> badges = new();

            foreach (var badge in userBadges)
            {
                badges.Add(context.Badges.Find(b => b._id == badge.Badge_id.ToString()).FirstOrDefault());
            };

            return badges;
        }

        public void AssignBadgeToUser(ObjectId userId, string badgeAbrv)
        {
            var badgeId = context.Badges.Find(x => x.ShortName == badgeAbrv).FirstOrDefault()._id;

            Users_Badges userBadge = new() 
            { 
                User_id = userId,
                Badge_id = ObjectId.Parse(badgeId),
                Date = DateTime.Now,
            };

            context.Users_Badges.ReplaceOne(x => x.User_id == userBadge.User_id && x.Badge_id == userBadge.Badge_id, userBadge, new ReplaceOptions { IsUpsert = true});
        }

        public long CountUserReadings(ObjectId userId)
        {
            var filter = Builders<Users_Books>.Filter.And(
                Builders<Users_Books>.Filter.Where(x => x.User_id == userId),
                Builders<Users_Books>.Filter.Where(x => x.List == "done"));

            return context.Users_Books.CountDocuments(filter);
        }

        public long CountUserReviews(ObjectId userId)
        {
            var filter = Builders<Review>.Filter.Eq("User_id", userId);

            return context.Reviews.CountDocuments(filter);
        }

        public int GetUserDaysOnPlatform(ObjectId userId)
        {
            var registerDate = context.Users.Find(u => u._id == userId).FirstOrDefault().RegisterDate;

            return (registerDate - DateTime.Now).Days;
        }
    }
}
