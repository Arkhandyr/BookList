using BookList.Helpers;
using BookList.Model;
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

        public ListService(IUserRepository userRepo, IListRepository listRepo, IHttpContextAccessor contextAccessor)
        {
            _listRepo = listRepo;
            _userRepo = userRepo;
        }

        public IResult AddToList(ListEntry entry)
        {

            Users_Books userBook = new()
            {
                _id = ObjectId.GenerateNewId(),
                Book_id = ObjectId.Parse(entry.book),
                User_id = _userRepo.GetByUsername(entry.user)._id,
                Date = DateTime.Now
            };

            _listRepo.AddToList(userBook);

            //if (user == null)
            //    return Results.BadRequest();

            return Results.Ok(new
            {
                message = "success"
            });
        }
    }
}
