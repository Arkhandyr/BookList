using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ReviewRepository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MongoDbContext context;

        public ReviewRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public void UpsertReview(Review review)
        {
            var filter = Builders<Review>.Filter.And(
                Builders<Review>.Filter.Where(u => u.User_id == review.User_id),
                Builders<Review>.Filter.Where(u => u.Book_id == review.Book_id));

            context.Reviews.ReplaceOne(filter, review, new ReplaceOptions { IsUpsert = true });
        }

        public DeleteResult DeleteReview(Review review)
        {
            var filter = Builders<Review>.Filter.And(
                Builders<Review>.Filter.Where(u => u.User_id == review.User_id),
                Builders<Review>.Filter.Where(u => u.Book_id == review.Book_id));

            return context.Reviews.DeleteOne(filter);
        }

        public List<Review> GetBookReviews(ObjectId bookId)
        {
            return context.Reviews.Find(r => r.Book_id == bookId).ToList();
        }

        public UpdateResult LikeReview(LikeEntry entry)
        {
            var filter = Builders<Review>.Filter.Where(r => r._id == entry.ReviewId);

            var update = Builders<Review>.Update.Push("Likes", entry.Username);

            return context.Reviews.UpdateOne(filter, update);
        }

        public UpdateResult DislikeReview(LikeEntry entry)
        {
            var filter = Builders<Review>.Filter.Where(r => r._id == entry.ReviewId);

            var update = Builders<Review>.Update.Pull("Likes", entry.Username);

            return context.Reviews.UpdateOne(filter, update);
        }
    }
}
