using BookList.Model;
using MongoDB.Bson;

namespace BookList
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> FilterBooks(string query);
        Task<Book> GetBookById(string id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(string id);
    }
}
