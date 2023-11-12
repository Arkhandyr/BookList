using BookList.Model;

namespace BookList.Service.UserService
{
    public interface IUserService
    {
        public IResult Register(RegisterUser user);

        public IResult Login(LoginUser user);

        public IResult User();

        public IResult Logout();

        public IResult GetByUsername(string username);

        public IResult GetToken();

        public Task<IEnumerable<User>> FilterByName(string filter, int page);

    }
}
