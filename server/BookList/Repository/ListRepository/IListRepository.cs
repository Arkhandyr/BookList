using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ListRepository
{
    public interface IListRepository
    {
        public void UpsertEntry(Users_Books users_Books);
        public DeleteResult RemoveEntry(Users_Books users_Books);
        public List<Book> GetUserList(string username, string list, int page);
        public bool GetBookStatus(Users_Books users_Books);
        public long[] CountBooks(string username);
    }
}
