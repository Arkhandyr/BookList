using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ReviewRepository
{
    public interface IReviewRepository
    {
        public void UpsertReview(Review review);
        public DeleteResult DeleteReview(Review review);
        public List<Review> GetBookReviews(ObjectId bookId);
        public UpdateResult LikeReview(LikeEntry entry);
        public UpdateResult DislikeReview(LikeEntry entry);
    }
}
