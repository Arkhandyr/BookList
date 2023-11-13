using BookList.Model;
using MongoDB.Driver;

namespace BookList.Repository.InteractionRepository
{
    public class InteractionRepository : IInteractionRepository
    {
        private readonly MongoDbContext context;

        public InteractionRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public void UpsertEntry(Follow follow)
        {
            var filter = Builders<Follow>.Filter.And(
                Builders<Follow>.Filter.Where(u => u.User_id == follow.User_id),
                Builders<Follow>.Filter.Where(u => u.User2_id == follow.User2_id));

            context.Follows.ReplaceOne(filter, follow, new ReplaceOptions { IsUpsert = true });
        }

        public DeleteResult RemoveEntry(Follow follow)
        {
            var filter = Builders<Follow>.Filter.And(
                Builders<Follow>.Filter.Where(u => u.User_id == follow.User_id),
                Builders<Follow>.Filter.Where(u => u.User2_id == follow.User2_id));

            return context.Follows.DeleteOne(filter);
        }

        public bool GetFollowStatus(Follow follow)
        {
            var filter = Builders<Follow>.Filter.And(
                Builders<Follow>.Filter.Where(u => u.User_id == follow.User_id),
                Builders<Follow>.Filter.Where(u => u.User2_id == follow.User2_id));

            return context.Follows.Find(filter).CountDocuments() > 0;

        }
    }
}
