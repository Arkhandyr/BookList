using BookList.Model;

namespace BookList.Service.UserService
{
    public interface IUserService
    {
        public IResult Register(RegisterUser user);

        public IResult Login(LoginUser user);
    }
}
