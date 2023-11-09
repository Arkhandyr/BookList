using BookList.Model;
using MongoDB.Bson;

namespace BookList.Service.BookService
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks(int page);
        public Task<Book> GetBookById(string id);
        public Task<IEnumerable<Book>> FilterByName(string filter, int page);
        public void AddBook(Book book);
        public void UpdateBook(Book book);
        public Task<IEnumerable<Book>> FilterByAuthor(string name);
    }
}
