using BookList.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookList
{
    public class BookRepository : IBookRepository
    {
        private readonly MongoDbContext context;
        private readonly ILogger<BookRepository> logger;
        private readonly int paginationSize = 5;


        public BookRepository(MongoDbContext context, ILogger<BookRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IEnumerable<Book>> GetTrendingBooks(int page)
        {
            var pipeline = context.Books.Aggregate()
                .Lookup("Users_Books", "_id", "Book_id", "Users_Books")
                .Unwind("Users_Books")
                .Match(Builders<BsonDocument>.Filter.In("Users_Books.List", new BsonArray { "planning", "reading" }))
                .Group(new BsonDocument { { "_id", "$_id" }, { "BookId", new BsonDocument("$first", "$_id") }, { "ReadingCount", new BsonDocument("$sum", 1) } })
                .Project(new BsonDocument("Id", "$BookId"))
                .Sort(Builders<BsonDocument>.Sort.Descending("ReadingCount"));

            var result = await pipeline.ToListAsync();

            var bookIds = result.Select(document => document["Id"].AsObjectId);

            var filter = Builders<Book>.Filter.In("_id", bookIds);
            var books = await context.Books.Find(filter).ToListAsync();
            var unreadBooks = await context.Books.Find(Builders<Book>.Filter.Not(filter)).ToListAsync();

            return books.Concat(unreadBooks).Skip((page - 1) * paginationSize).Take(paginationSize);

        }

        public async Task<IEnumerable<Book>> GetTopBooks(int page)
        {
            var pipeline = context.Books.Aggregate()
                .Lookup("Users_Books", "_id", "Book_id", "Users_Books")
                .Unwind("Users_Books")
                .Match(Builders<BsonDocument>.Filter.In("Users_Books.List", new BsonArray { "done" }))
                .Group(new BsonDocument { { "_id", "$_id" }, { "BookId", new BsonDocument("$first", "$_id") }, { "ReadingCount", new BsonDocument("$sum", 1) } })
                .Project(new BsonDocument("Id", "$BookId"))
                .Sort(Builders<BsonDocument>.Sort.Descending("ReadingCount"));

            var result = await pipeline.ToListAsync();

            var bookIds = result.Select(document => document["Id"].AsObjectId);

            var filter = Builders<Book>.Filter.In("_id", bookIds);
            var books = await context.Books.Find(filter).ToListAsync();
            var unreadBooks = await context.Books.Find(Builders<Book>.Filter.Not(filter)).ToListAsync();

            return books.Concat(unreadBooks).Skip((page - 1) * paginationSize).Take(paginationSize);

        }

        public async Task<IEnumerable<Book>> FilterByName(string query, int page)
        {
            return await context.Books.Find(x => x.Title.ToLower().Contains(query) || x.Author.ToLower().Contains(query)).Skip((page - 1) * paginationSize).Limit(paginationSize).ToListAsync();
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

        public async Task<IEnumerable<Book>> FilterByAuthor(string name)
        {
            return await context.Books.Find(b => b.Author == name).ToListAsync();
        }
    }
}
