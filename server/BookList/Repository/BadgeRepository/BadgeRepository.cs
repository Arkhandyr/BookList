using BookList.Model;
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

            List<Users_Badges> userBadges =  context.Users_Badges.Find(u => u.User_id == user._id).ToList();

            List<Badge> badges = new();

            foreach (var badge in userBadges)
            {
                badges.Add(context.Badges.Find(b => b._id == badge.Badge_id.ToString()).FirstOrDefault());
            };

            return badges;
        }
    }
}
