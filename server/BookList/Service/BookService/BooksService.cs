using BookList.Model;

namespace BookList.Service.BookService
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _bookRepo;

        public BooksService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Task<IEnumerable<Book>> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
        }

        public Task<IEnumerable<Book>> FilterBooks(string filter)
        {
            return _bookRepo.FilterBooks(filter);
        }

        public Task<Book> GetBookById(Guid id)
        {
            return _bookRepo.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepo.AddBook(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.UpdateBook(book);
        }

        public void DeleteBook(Guid id)
        {
            _bookRepo.DeleteBook(id);
        }
    }
}
