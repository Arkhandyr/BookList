using BookList.Helpers;
using BookList.Model;
using BookList.Repository.ListRepository;
using BookList.Repository.ReviewRepository;
using BookList.Repository.UserRepository;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace BookList.Service.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IUserRepository _userRepo;

        public ReviewService(IUserRepository userRepo, IReviewRepository reviewRepo, IHttpContextAccessor contextAccessor)
        {
            _reviewRepo = reviewRepo;
            _userRepo = userRepo;
        }

        public IResult AddReview(ReviewEntry entry)
        {
            Review review = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = _userRepo.GetByUsername(entry.Username)._id,
                Text = entry.Text,
                Date = DateTime.Now
            };

            _reviewRepo.UpsertReview(review);

            //if (user == null)
            //    return Results.BadRequest();

            return Results.Ok(new { message = "success" });
        }

        public IResult DeleteReview(ReviewEntry entry)
        {
            Review review = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = _userRepo.GetByUsername(entry.Username)._id
            };

            var response = _reviewRepo.DeleteReview(review);

            return Results.Ok(response);
        }

        public IResult GetBookReviews(string bookId)
        {
            List<Review> reviews = _reviewRepo.GetBookReviews(ObjectId.Parse(bookId));

            foreach (Review review in reviews)
            {
                review.User = _userRepo.GetById(review.User_id);
            }

            return Results.Ok(reviews);
        }
    }
}
