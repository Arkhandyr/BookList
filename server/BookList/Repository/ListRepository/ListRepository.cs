using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ListRepository
{
    public class ListRepository : IListRepository
    {
        private readonly MongoDbContext context;
        private readonly int paginationSize = 5;

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

        public List<Book> GetUserList(string username, string list, int page)
        {
            User user = context.Users.Find(u => u.Username == username).FirstOrDefault();

            List<Users_Books> booksInList = context.Users_Books.Find(u => u.User_id == user._id && u.List == list).Skip((page - 1) * paginationSize).Limit(paginationSize).ToList();

            List<Book> books = new();

            foreach (var book in booksInList)
            {
                books.Add(context.Books.Find(b => b._id == book.Book_id.ToString()).FirstOrDefault());
            }

            return books;
        }

        public bool GetBookStatus(Users_Books users_Books)
        {
            return context.Users_Books.CountDocuments(u => u.User_id == users_Books.User_id && u.Book_id == users_Books.Book_id) > 0;
        }


        public long[] CountBooks(string username)
        {
            User user = context.Users.Find(u => u.Username == username).FirstOrDefault();

            return new long[3]
            {
                context.Users_Books.CountDocuments(u => u.User_id == user._id && u.List == "reading"),
                context.Users_Books.CountDocuments(u => u.User_id == user._id && u.List == "planning"),
                context.Users_Books.CountDocuments(u => u.User_id == user._id && u.List == "done")
            };
        }
    }
}
