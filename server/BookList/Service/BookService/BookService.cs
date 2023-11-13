using BookList.Model;
using MongoDB.Bson;

namespace BookList.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Task<IEnumerable<Book>> GetTrendingBooks(int page)
        {
            return _bookRepo.GetTrendingBooks(page);
        }

        public Task<IEnumerable<Book>> GetTopBooks(int page)
        {
            return _bookRepo.GetTopBooks(page);
        }

        public Task<IEnumerable<Book>> FilterByName(string filter, int page)
        {
            return _bookRepo.FilterByName(filter, page);
        }

        public Task<Book> GetBookById(string id)
        {
            return _bookRepo.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.UpdateBook(book);
        }


        public Task<IEnumerable<Book>> FilterByAuthor(string name)
        {
            name = name.Replace('-', '.').Replace('_', ' ');

            return _bookRepo.FilterByAuthor(name);
        }
    }
}
