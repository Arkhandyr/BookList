using BookList.Model;

namespace BookList.Service.ListService
{
    public interface IListService
    {
        public IResult AddToList(ListEntry entry);

        public IResult RemoveFromList(ListEntry entry);

        public IResult GetUserList(string username, string list, int page);

        public IResult GetBookStatus(string bookId, string username);

        public IResult CountBooks(string username);
    }
}
