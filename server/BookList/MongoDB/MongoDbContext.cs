using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }

        private IMongoDatabase _database { get; }

        public MongoDbContext(IConfiguration config)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            var dbSettings = config.GetSection("BookListDatabase").Get<BookListDatabaseSettings>();
            try
            {
                var mongoClient = new MongoClient(dbSettings.ConnectionString);
                _database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
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
    }
}
