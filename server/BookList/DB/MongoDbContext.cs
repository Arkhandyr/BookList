using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList
{
    public class MongoDbContext
    {
        public static string? ConnectionString { get; set; }
        public static string? DatabaseName { get; set; }
        private IMongoDatabase _database { get; }

        public MongoDbContext()
        {
            try
            {
                var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MongoConnectionString") ?? "mongodb://localhost:27017");
                _database = mongoClient.GetDatabase("BookList");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor MongoDB.", ex);
            }
        }

        public IMongoCollection<Book> Books
        {
            get
            {
                return _database.GetCollection<Book>("Books");
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }

        public IMongoCollection<Users_Books> Users_Books
        {
            get
            {
                return _database.GetCollection<Users_Books>("Users_Books");
            }
        }

        public IMongoCollection<Review> Reviews
        {
            get
            {
                return _database.GetCollection<Review>("Reviews");
            }
        }

        public IMongoCollection<Badge> Badges
        {
            get
            {
                return _database.GetCollection<Badge>("Badges");
            }
        }

        public IMongoCollection<Users_Badges> Users_Badges
        {
            get
            {
                return _database.GetCollection<Users_Badges>("Users_Badges");
            }
        }

        public IMongoCollection<Author> Authors
        {
            get
            {
                return _database.GetCollection<Author>("Authors");
            }
        }

        public IMongoCollection<Follow> Follows
        {
            get
            {
                return _database.GetCollection<Follow>("Follows");
            }
        }
    }
}
