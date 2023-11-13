using BookList.Model;
using MongoDB.Bson;

namespace BookList
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetTrendingBooks(int page);
        Task<IEnumerable<Book>> GetTopBooks(int page);
        Task<IEnumerable<Book>> FilterByName(string query, int page);
        Task<Book> GetBookById(string id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        Task<IEnumerable<Book>> FilterByAuthor(string name);
    }
}
