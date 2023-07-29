using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepo, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        public IResult Register(RegisterUser registerUser)
        {
            User user = new()
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
                Picture = registerUser.Picture,
            };

            return Results.Created("success", _userRepo.Register(user));
        }

        public IResult Login(LoginUser loginUser)
        {
            User user = _userRepo.GetByEmail(loginUser.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return Results.BadRequest();

            var jwt = _jwtService.Generate(user.Id);

            return Results.Ok(new
            {
                message = "success"
            });
        }
    }
}
