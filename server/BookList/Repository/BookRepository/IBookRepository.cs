using BookList.Model;

namespace BookList
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> FilterBooks(string query);
        Task<Book> GetBookById(Guid id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Guid id);
    }
}
