using BookList.Helpers;
using BookList.Model;
using BookList.Repository.InteractionRepository;
using BookList.Repository.UserRepository;

namespace BookList.Service.InteractionService
{
    public class InteractionService : IInteractionService
    {
        private readonly IUserRepository _userRepo;
        private readonly IInteractionRepository _interactionRepo;

        public InteractionService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IResult Follow(FollowEntry entry)
        {

            Follow follow = new()
            {
                User_id = _userRepo.GetByUsername(entry.User)._id,
                User2_id = _userRepo.GetByUsername(entry.User2)._id,
                Date = DateTime.Now
            };

            _interactionRepo.UpsertEntry(follow);

            return Results.Ok(new { message = "success" });
        }

        public IResult Unfollow(FollowEntry entry)
        {
            Follow follow = new()
            {
                User_id = _userRepo.GetByUsername(entry.User)._id,
                User2_id = _userRepo.GetByUsername(entry.User2)._id
            };

            var response = _interactionRepo.RemoveEntry(follow);

            return Results.Ok(response);
        }
    }
}
