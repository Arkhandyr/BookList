using BookList.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookList.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext context;

        public UserRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public User Register(User user)
        {
            context.Users.InsertOne(user);

            return user;
        }

        public User GetByEmail(string email)
        {
            return context.Users.Find(u => u.Email == email).FirstOrDefault();
        }

        public User GetById(ObjectId id)
        {
            return context.Users.Find(u => u._id == id).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return context.Users.Find(u => u.Username == username).FirstOrDefault();
        }
    }
}
