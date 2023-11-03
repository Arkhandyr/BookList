using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MongoDbContext context;

        public AuthorRepository(MongoDbContext context)
        {
            this.context = context;
        }


        public Author GetByName(string name)
        {
            var filter = Builders<Author>.Filter.Eq("Name", name);
            var options = new FindOptions { Collation = new Collation("en", strength: CollationStrength.Primary) };
            return context.Authors.Find(filter, options).FirstOrDefault();
        }
    }
}
