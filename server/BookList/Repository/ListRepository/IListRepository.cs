using BookList.Model;

namespace BookList.Repository.ListRepository
{
    public interface IListRepository
    {
        public void AddToList(Users_Books users_Books);
    }
}
