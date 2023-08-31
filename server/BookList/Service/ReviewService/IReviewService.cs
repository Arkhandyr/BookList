using BookList.Model;

namespace BookList.Service.ReviewService
{
    public interface IReviewService
    {
        public IResult AddReview(ReviewEntry entry);

        public IResult DeleteReview(ReviewEntry entry);

        public IResult GetBookReviews(string bookId);
    }
}
