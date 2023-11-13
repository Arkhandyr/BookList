using BookList.Model;
using MongoDB.Driver;

namespace BookList.Repository.InteractionRepository
{
    public interface IInteractionRepository
    {
        public void UpsertEntry(Follow follow);
        public DeleteResult RemoveEntry(Follow follow);
        public bool GetFollowStatus (Follow follow);
    }
}
