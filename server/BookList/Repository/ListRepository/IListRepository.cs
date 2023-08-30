using BookList.Model;
using MongoDB.Driver;

namespace BookList.Repository.ListRepository
{
    public interface IListRepository
    {
        public ReplaceOneResult UpsertEntry(Users_Books users_Books);
        public DeleteResult RemoveEntry(Users_Books users_Books);
        public List<List<Book>> GetUserLists(string username);
        public bool GetBookStatus(Users_Books users_Books);
    }
}
