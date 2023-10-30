using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList
{
    public class BookRepository : IBookRepository
    {
        private readonly MongoDbContext context;
        private readonly int paginationSize = 5;

        public BookRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks(int page)
        {
            return await context.Books.Find(x => true).Skip((page - 1) * paginationSize).Limit(paginationSize).ToListAsync();
            //return await context.Books.Find(x => true).SortBy(x => x.ReadingNow).Skip((page - 1) * paginationSize).Limit(paginationSize).ToListAsync();
        }

        public long GetBookCount()
        {
            return context.Users_Books.EstimatedDocumentCount(); //usar futuramente para paginamento
        }

        public async Task<IEnumerable<Book>> FilterBooks(string query)
        {
            return await context.Books.Find(x => x.Title.ToLower().Contains(query) || x.Author.ToLower().Contains(query)).ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            Book book = context.Books.Find(x => x._id == id).FirstOrDefault();

            book.InteractionData = new InteractionData()
            {
                Planning = context.Users_Books.CountDocuments(u => u.Book_id.ToString() == id && u.List == "planning"),
                Reading = context.Users_Books.CountDocuments(u => u.Book_id.ToString() == id && u.List == "reading"),
                Done = context.Users_Books.CountDocuments(u => u.Book_id.ToString() == id && u.List == "done")
            };

            return book;
        }

        public void AddBook(Book book)
        {
            context.Books.InsertOne(book);
        }

        public void UpdateBook(Book book)
        {
            context.Books.ReplaceOne(x => x._id == book._id, book);
        }
    }
}
