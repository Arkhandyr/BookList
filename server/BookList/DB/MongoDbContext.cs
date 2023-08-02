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

        public MongoDbContext(IConfiguration config)
        {
            var dbSettings = config.GetSection("MongoDatabase").Get<MongoDatabaseSettings>();
            try
            {
                var mongoClient = new MongoClient(dbSettings.ConnectionString);
                _database = mongoClient.GetDatabase(dbSettings.DatabaseName);
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
    }
}
