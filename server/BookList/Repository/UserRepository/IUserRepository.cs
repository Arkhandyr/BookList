using BookList.Model;

namespace BookList.Repository.UserRepository
{
    public interface IUserRepository
    {
        public User Register(User user);

        public User GetByEmail(string email);
    }
}
