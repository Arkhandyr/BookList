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

        public Author GetById(string id)
        {
            return context.Authors.Find(u => u._id.ToString() == id).FirstOrDefault();
        }

        public Author GetByName(string name)
        {
            return context.Authors.Find(a => a.Name == name).FirstOrDefault();
        }
    }
}
