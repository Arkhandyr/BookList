using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ListRepository
{
    public class ListRepository : IListRepository
    {
        private readonly MongoDbContext context;

        public ListRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public void UpsertEntry(Users_Books users_Books)
        {
            var filter = Builders<Users_Books>.Filter.And(
                Builders<Users_Books>.Filter.Where(u => u.User_id == users_Books.User_id),
                Builders<Users_Books>.Filter.Where(u => u.Book_id == users_Books.Book_id));

            context.Users_Books.ReplaceOne(filter, users_Books, new ReplaceOptions { IsUpsert = true });

            //var filterBook = Builders<Book>.Filter.Where(u => u.User_id == users_Books.User_id);

            //context.Books.ReplaceOne(filterBook, users_Books, new ReplaceOptions { IsUpsert = true });
        }

        public DeleteResult RemoveEntry(Users_Books users_Books)
        {
            var filter = Builders<Users_Books>.Filter.And(
                Builders<Users_Books>.Filter.Where(u => u.User_id == users_Books.User_id),
                Builders<Users_Books>.Filter.Where(u => u.Book_id == users_Books.Book_id));

            return context.Users_Books.DeleteOne(filter);
        }

        public List<List<Book>> GetUserLists(string username)
        {
            User user = context.Users.Find(u => u.Username == username).FirstOrDefault();

            List<Users_Books> lists = context.Users_Books.Find(u => u.User_id == user._id).ToList();

            List<List<Book>> books = new()
            {
                new List<Book>(),
                new List<Book>(),
                new List<Book>()    
            };

            foreach (var item in lists)
            {
                switch (item.List)
                {
                    case "reading":
                        books[0].Add(context.Books.Find(b => b._id == item.Book_id.ToString()).FirstOrDefault());
                        break;

                    case "planning":
                        books[1].Add(context.Books.Find(b => b._id == item.Book_id.ToString()).FirstOrDefault());
                        break;

                    case "done":
                        books[2].Add(context.Books.Find(b => b._id == item.Book_id.ToString()).FirstOrDefault());
                        break;

                    default:
                        break;
                }
            }

            return books;
        }

        public bool GetBookStatus(Users_Books users_Books)
        {
            return context.Users_Books.CountDocuments(u => u.User_id == users_Books.User_id && u.Book_id == users_Books.Book_id) > 0;
        }
    }
}
