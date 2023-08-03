using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace BookList.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IUserRepository userRepo, IJwtService jwtService, IHttpContextAccessor contextAccessor)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _contextAccessor = contextAccessor;
        }

        public IResult Register(RegisterUser registerUser)
        {
            User user = new()
            {
                Username = registerUser.Username,
                RealName = registerUser.RealName,
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

            _contextAccessor.HttpContext.Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Results.Ok(new
            {
                message = "success"
            });
        }

        public IResult User()
        {
            try
            {

                var jwt = _contextAccessor.HttpContext.Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);
                var user = _userRepo.GetById(userId);

                return Results.Ok(user);
            }
            catch (Exception)
            {
                return Results.Unauthorized();
            }
        }

        public IResult Logout()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete("jwt");

            return Results.Ok(new
            {
                message = "success"
            });
        }

        public IResult GetByUsername(string username)
        {
            User user = _userRepo.GetByUsername(username);

            if (user == null)
                return Results.BadRequest();

            return Results.Ok(user);
        }
    }
}
