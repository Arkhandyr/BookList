using BookList.Helpers;
using BookList.Model;
using BookList.Repository.BadgeRepository;
using BookList.Repository.ListRepository;
using BookList.Repository.UserRepository;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace BookList.Service.ListService
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBadgeRepository _badgeRepo;

        public ListService(IUserRepository userRepo, IListRepository listRepo, IBadgeRepository badgeRepo, IHttpContextAccessor contextAccessor)
        {
            _listRepo = listRepo;
            _userRepo = userRepo;
            _badgeRepo = badgeRepo;
        }

        public IResult AddToList(ListEntry entry)
        {
            ObjectId userId = _userRepo.GetByUsername(entry.Username)._id;

            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = userId,
                List = entry.ListName,
                Date = DateTime.Now
            };

            _listRepo.UpsertEntry(userBook);

            switch (_badgeRepo.CountUserReadings(userId))
            {
                case 1:
                    _badgeRepo.AssignBadgeToUser(userId, "LEI1");
                    break;
                case 5:
                    _badgeRepo.AssignBadgeToUser(userId, "LEI2");
                    break;
                case 10:
                    _badgeRepo.AssignBadgeToUser(userId, "LEI3");
                    break;
                default:
                    break;
            }

            return Results.Ok(new { message = "success" });
        }

        public IResult RemoveFromList(ListEntry entry)
        {
            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = _userRepo.GetByUsername(entry.Username)._id
            };

            var response = _listRepo.RemoveEntry(userBook);

            return Results.Ok(response);
        }

        public IResult GetUserList(string username, string list, int page)
        {
            List<Book> userList = _listRepo.GetUserList(username, list, page);

            return Results.Ok(userList);
        }

        public IResult GetBookStatus(string bookId, string username)
        {
            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(bookId),
                User_id = _userRepo.GetByUsername(username)._id
            };

            var bookStatus = _listRepo.GetBookStatus(userBook);

            return Results.Ok(bookStatus);
        }

        public IResult CountBooks(string username)
        {
            var count = _listRepo.CountBooks(username);

            return Results.Ok(count);
        }
    }
}
