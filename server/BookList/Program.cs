#region Configs
using BookList;
using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using BookList.Service.BookService;
using BookList.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<MongoDbContext>();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
.WithOpenApi(operation => new(operation)
    {
        OperationId = "Catalog",
        Summary = "Cat�logo da p�gina inicial",
        Description = "Descri��o de tudo que tem asdfhjnmiusoadhfniouasdhtgiodstjgmiaodstjgmas muita coisa texto longo lorem ipsum ligma sugma"
    });

app.MapGet("/catalog/{filter}", async ([FromServices] IBookService service, string filter) => 
    await service.FilterBooks(filter))
.WithName("FilterBooks");

app.MapGet("/book/{id}", async ([FromServices] IBookService service, string id) =>
    await service.GetBookById(id))
.WithName("GetBookById");

app.MapPost("/add", ([FromServices] IBookService service, Book book) =>
{
    service.AddBook(book);
})
.WithName("AddBook");

app.MapPost("/register", ([FromServices] IUserService service, RegisterUser user) =>
{
    return service.Register(user);
})
.WithName("Register");

app.MapPost("/login", ([FromServices] IUserService service, LoginUser user) =>
{
    return service.Login(user);
})
.WithName("Login");

app.MapGet("/user", ([FromServices] IUserService service) =>
    service.User())
.WithName("User");

app.MapGet("/profile/{username}", ([FromServices] IUserService service, string username) =>
    service.GetByUsername(username))
.WithName("GetByUsername");

app.MapPost("/logout", ([FromServices] IUserService service) =>
{
    return service.Logout();
})
.WithName("Logout");
#endregion

app.Run();