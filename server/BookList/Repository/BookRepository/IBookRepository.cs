using BookList.Model;
using MongoDB.Bson;

namespace BookList
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks(int page);
        Task<IEnumerable<Book>> FilterByName(string query);
        Task<Book> GetBookById(string id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        Task<IEnumerable<Book>> FilterByAuthor(string name);
    }
}
