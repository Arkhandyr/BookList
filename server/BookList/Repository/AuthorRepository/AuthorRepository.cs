using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly MongoDbContext context;
        private readonly int paginationSize = 5;

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

        public async Task<IEnumerable<Author>> FilterByName(string query, int page)
        {
            return await context.Authors.Find(x => x.Name.ToLower().Contains(query)).Skip((page - 1) * paginationSize).Limit(paginationSize).ToListAsync();
        }
    }
}
