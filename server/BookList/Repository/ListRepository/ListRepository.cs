using BookList.Model;
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

        public void AddToList(Users_Books users_Books)
        {
            context.Users_Books.InsertOne(users_Books);
        }

        public List<Book> GetUserLists(string username)
        {
            User user = context.Users.Find(u => u.Username == username).FirstOrDefault();

            List<Users_Books> lists = context.Users_Books.Find(u => u.User_id == user._id).ToList();

            List<Book> books = new();

            foreach (var item in lists)
            {
                books.Add(context.Books.Find(b => b._id == item.Book_id.ToString()).FirstOrDefault());
            }

            return books;
        }
    }
}
