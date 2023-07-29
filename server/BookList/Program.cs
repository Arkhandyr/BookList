#region Configs
using BookList;
using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using BookList.Service.BookService;
using BookList.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddTransient<MongoDbContext>();
builder.Services.AddDbContext<SqlServerDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserDb")));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBooksService, BooksService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().WithMethods("GET", "POST", "PUT", "DELETE"));
}
#endregion

#region Endpoints
app.MapGet("/catalog", async ([FromServices] IBooksService service) => 
    await service.GetAllBooks()).WithName("Catalog");

app.MapGet("/catalog/{filter}", async ([FromServices] IBooksService service, string filter) => 
    await service.FilterBooks(filter)).WithName("FilterBooks");

app.MapGet("/book/{id}", async ([FromServices] IBooksService service, Guid id) =>
    await service.GetBookById(id)).WithName("GetBookById");

app.MapPost("/add", ([FromServices] IBooksService service, Book book) =>
{
    service.AddBook(book);
})
.WithName("AddBook");

app.MapPut("/update", ([FromServices] IBooksService service, Book book) =>
{
    service.UpdateBook(book);
})
.WithName("UpdateBook");

app.MapDelete("/books/{id}", ([FromServices] IBooksService service, Guid id) =>
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
#endregion

app.Run();