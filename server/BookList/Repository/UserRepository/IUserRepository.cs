using BookList.Model;
using MongoDB.Bson;

namespace BookList.Repository.UserRepository
{
    public interface IUserRepository
    {
        public User Register(User user);

        public User GetByEmail(string email);

        public User GetById(ObjectId id);

        public User GetByUsername(string username);
    }
}
