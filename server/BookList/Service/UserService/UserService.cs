using BookList.Model;
using BookList.Repository.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public ServiceResult<User> Register(RegisterUser registerUser)
        {
            User user = new()
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
                Picture = registerUser.Picture,
            };

            return new ServiceResult<User>(true, _userRepo.Register(user));
        }

        public ServiceResult<User> Login(LoginUser loginUser)
        {
            User user = _userRepo.GetByEmail(loginUser.Email);

            return new ServiceResult<User>(true, user);
        }
    }
}
