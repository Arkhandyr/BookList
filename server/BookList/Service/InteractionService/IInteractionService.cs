using BookList.Model;

namespace BookList.Service.InteractionService
{
    public interface IInteractionService
    {
        public IResult Follow(FollowEntry entry);

        public IResult Unfollow(FollowEntry entry);

        public IResult GetFollowStatus(string user, string user2);
    }
}
