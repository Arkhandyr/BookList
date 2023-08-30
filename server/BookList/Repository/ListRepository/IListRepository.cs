using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.ListRepository
{
    public interface IListRepository
    {
        public void UpsertEntry(Users_Books users_Books);
        public DeleteResult RemoveEntry(Users_Books users_Books);
        public List<List<Book>> GetUserLists(string username);
        public bool GetBookStatus(Users_Books users_Books);
    }
}
