namespace BookList.Service.BookService
{
    public interface IBooksService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(Guid id);
        public Task<IEnumerable<Book>> FilterBooks(string filter);
        public void AddBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Guid id);
    }
}
