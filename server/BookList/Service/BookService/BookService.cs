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

        public Task<IEnumerable<Book>> GetAllBooks(int page)
        {
            return _bookRepo.GetAllBooks(page);
        }

        public Task<IEnumerable<Book>> FilterByName(string filter)
        {
            return _bookRepo.FilterByName(filter);
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


        public Task<IEnumerable<Book>> FilterByAuthor(string id)
        {
            return _bookRepo.FilterByAuthor(id);
        }
    }
}
