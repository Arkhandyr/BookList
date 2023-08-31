using BookList.Model;

namespace BookList.Service.ListService
{
    public interface IListService
    {
        public IResult AddToList(ListEntry entry);

        public IResult RemoveFromList(ListEntry entry);

        public IResult GetUserLists(string username);

        public IResult GetBookStatus(string bookId, string username);
    }
}
