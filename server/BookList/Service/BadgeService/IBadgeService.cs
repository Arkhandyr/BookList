namespace BookList.Service.BadgeService
{
    public interface IBadgeService
    {
        public IResult GetUserBadges(string username);
    }
}
