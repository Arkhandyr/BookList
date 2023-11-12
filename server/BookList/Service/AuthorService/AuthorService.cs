using BookList.Model;
using BookList.Service.AuthorService;
using BookList.Repository.AuthorRepository;
using System.Web;
using System.Net;

namespace BookList.Service.UserService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IResult GetByName(string name)
        {
            name = name.Replace('-', '.').Replace('_', ' ');

            Author author = _authorRepo.GetByName(name);

            if (author == null)
                return Results.NotFound();

            return Results.Ok(author);
        }

        public Task<IEnumerable<Author>> FilterByName(string filter, int page)
        {
            return _authorRepo.FilterByName(filter, page);
        }
    }
}
