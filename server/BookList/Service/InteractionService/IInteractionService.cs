using BookList.Model;

namespace BookList.Service.InteractionService
{
    public interface IInteractionService
    {
        public IResult Follow(FollowEntry entry);

        public IResult Unfollow(FollowEntry entry);
    }
}
