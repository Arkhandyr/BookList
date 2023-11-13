using BookList.Helpers;
using BookList.Model;
using BookList.Repository.BadgeRepository;
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
        private readonly IBadgeRepository _badgeRepo;   

        public ReviewService(IUserRepository userRepo, IReviewRepository reviewRepo, IBadgeRepository badgeRepo, IHttpContextAccessor contextAccessor)
        {
            _reviewRepo = reviewRepo;
            _userRepo = userRepo;
            _badgeRepo = badgeRepo;
        }

        public IResult AddReview(ReviewEntry entry)
        {
            ObjectId userId = _userRepo.GetByUsername(entry.Username)._id;

            Review review = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = userId,
                Text = entry.Text,
                Rating = entry.Rating,
                Date = DateTime.Now,
                Likes = new List<string>()
            };

            _reviewRepo.UpsertReview(review);

            switch (_badgeRepo.CountUserReviews(userId))
            {
                case 1:
                    _badgeRepo.AssignBadgeToUser(userId, "REV1");
                    break;
                case 5:
                    _badgeRepo.AssignBadgeToUser(userId, "REV2");
                    break;
                case 10:
                    _badgeRepo.AssignBadgeToUser(userId, "REV3");
                    break;
                default:
                    break;
            }

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

        public IResult LikeReview(LikeEntry entry)
        {
            _reviewRepo.LikeReview(entry);

            return Results.Ok(new { message = "success" });
        }

        public IResult DislikeReview(LikeEntry entry)
        {
            _reviewRepo.DislikeReview(entry);

            return Results.Ok(new { message = "success" });
        }
    }
}
