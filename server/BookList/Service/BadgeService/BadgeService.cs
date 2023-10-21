using BookList.Model;
using BookList.Repository.BadgeRepository;
using BookList.Repository.UserRepository;

namespace BookList.Service.BadgeService
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepo;
        private readonly IUserRepository _userRepo;

        public BadgeService(IUserRepository userRepo, IBadgeRepository badgeRepo, IHttpContextAccessor contextAccessor)
        {
            _badgeRepo = badgeRepo;
            _userRepo = userRepo;
        }

        public IResult GetUserBadges(string username)
        {
            List<Badge> userBadges = _badgeRepo.GetUserBadges(username);

            return Results.Ok(userBadges);
        }
    }
}
