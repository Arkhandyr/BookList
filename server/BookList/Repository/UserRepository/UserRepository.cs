using BookList.Model;

namespace BookList.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlServerDbContext context;

        public UserRepository(SqlServerDbContext context)
        {
            this.context = context;
        }

        public User Register(User user)
        {
            context.Users.Add(user);
            user.Id = context.SaveChanges();

            return user;
        }
    }
}
