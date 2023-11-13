using BookList.Model;
using MongoDB.Bson;
using static BookList.Repository.BadgeRepository.BadgeRepository;

namespace BookList.Repository.BadgeRepository
{
    public interface IBadgeRepository
    {
        public List<Badge> GetUserBadges(string username);
        public void AssignBadgeToUser(ObjectId userId, string badgeAbrv);
        public long CountUserReadings(ObjectId userId);
        public long CountUserReviews(ObjectId userId);
        public int GetUserDaysOnPlatform(ObjectId userId);
    }
}
