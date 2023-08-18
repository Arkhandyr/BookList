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
        Description = "Endpoint respons�vel por trazer o cat�logo completo de livros para a p�gina inicial"
    });

app.MapGet("/catalog/{filter}", async ([FromServices] IBookService service, string filter) =>
    await service.FilterBooks(filter))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtro da p�gina inicial",
    Description = "Endpoint respons�vel por filtrar os livros mostrados na p�gina inicial",
    Parameters = new List<OpenApiParameter>()
    {
        new OpenApiParameter() { Name = "Filter", Description = "Nome de um livro ou autor" }
    }
});

app.MapGet("/book/{id}", async ([FromServices] IBookService service, string id) =>
    await service.GetBookById(id))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetBookById",
    Summary = "Seleciona livro",
    Description = "Endpoint respons�vel por trazer as informa��es do livro selecionado para a p�gina de obra"
});

app.MapPost("/add", ([FromServices] IBookService service, Book book) =>
{
    service.AddBook(book);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddBook",
    Summary = "Adiciona livro",
    Description = "Endpoint respons�vel por adicionar um novo livro no banco de dados"
});

app.MapPost("/register", ([FromServices] IUserService service, RegisterUser user) =>
{
    return service.Register(user);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Register",
    Summary = "Registro",
    Description = "Endpoint respons�vel por adicionar um novo usu�rio no banco de dados"
});

app.MapPost("/login", ([FromServices] IUserService service, LoginUser user) =>
{
    return service.Login(user);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Login",
    Summary = "Login",
    Description = "Endpoint respons�vel por gerar um token para o usu�rio realizando login"
});

app.MapGet("/user", ([FromServices] IUserService service) =>
    service.User())
.WithOpenApi(operation => new(operation)
{
    OperationId = "User",
    Summary = "Usu�rio",
    Description = "Endpoint respons�vel por trazer as informa��es de um usu�rio logado"
});

app.MapGet("/profile/{username}", ([FromServices] IUserService service, string username) =>
    service.GetByUsername(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetByUsername",
    Summary = "Seleciona usu�rio",
    Description = "Endpoint respons�vel por trazer as informa��es do usu�rio selecionado para a p�gina de perfil de usu�rio"
});

app.MapPost("/logout", ([FromServices] IUserService service) =>
{
    return service.Logout();
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Logout",
    Summary = "Logout",
    Description = "Endpoint respons�vel por deslogar o usu�rio da sess�o"
});
#endregion

app.Run();