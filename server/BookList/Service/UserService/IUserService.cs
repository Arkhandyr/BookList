using BookList.Model;

namespace BookList.Service.UserService
{
    public interface IUserService
    {
        public ServiceResult<User> Register(RegisterUser user);

        public ServiceResult<User> Login(LoginUser user);
    }
}
