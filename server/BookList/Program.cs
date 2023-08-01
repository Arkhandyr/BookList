#region Configs
using BookList;
using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using BookList.Service.BookService;
using BookList.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<MongoDbContext>();
builder.Services.AddDbContext<SqlServerDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:4200")
        .WithMethods("GET", "POST", "PUT", "DELETE"));
}

var httpContext = app.Services.GetRequiredService<IHttpContextAccessor>().HttpContext;
#endregion

#region Endpoints
app.MapGet("/catalog", async ([FromServices] IBookService service) => 
    await service.GetAllBooks())
.WithName("Catalog");

app.MapGet("/catalog/{filter}", async ([FromServices] IBookService service, string filter) => 
    await service.FilterBooks(filter))
.WithName("FilterBooks");

app.MapGet("/book/{id}", async ([FromServices] IBookService service, Guid id) =>
    await service.GetBookById(id))
.WithName("GetBookById");

app.MapPost("/add", ([FromServices] IBookService service, Book book) =>
{
    service.AddBook(book);
})
.WithName("AddBook");

app.MapPut("/update", ([FromServices] IBookService service, Book book) =>
{
    service.UpdateBook(book);
})
.WithName("UpdateBook");

app.MapDelete("/books/{id}", ([FromServices] IBookService service, Guid id) =>
{
    service.DeleteBook(id);
})
.WithName("DeleteBook");

app.MapPost("/user/register", ([FromServices] IUserService service, RegisterUser user) =>
{
    return service.Register(user);
})
.WithName("Register");

app.MapPost("/user/login", ([FromServices] IUserService service, LoginUser user) =>
{
    return service.Login(user);
})
.WithName("Login");

app.MapGet("/user", ([FromServices] IUserService service) =>
    service.User())
.WithName("User");

app.MapGet("/logout", ([FromServices] IUserService service) =>
    service.Logout())
.WithName("Logout");
#endregion

app.Run();