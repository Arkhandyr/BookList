using BookList.Model;
using MongoDB.Bson;

namespace BookList
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks(int page);
        Task<IEnumerable<Book>> FilterBooks(string query);
        Task<Book> GetBookById(string id);
        void AddBook(Book book);
        void UpdateBook(Book book);
    }
}
