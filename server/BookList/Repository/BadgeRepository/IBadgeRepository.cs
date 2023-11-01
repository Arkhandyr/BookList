using BookList.Model;

namespace BookList.Repository.BadgeRepository
{
    public interface IBadgeRepository
    {
        public List<Badge> GetUserBadges(string username);
    }
}
