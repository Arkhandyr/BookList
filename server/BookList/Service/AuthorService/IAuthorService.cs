using BookList.Model;

namespace BookList.Service.AuthorService
{
    public interface IAuthorService
    {
        public IResult GetByName(string name);
        public Task<IEnumerable<Author>> FilterByName(string filter, int page);
    }
}
