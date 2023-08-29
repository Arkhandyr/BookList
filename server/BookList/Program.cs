#region Configs
using BookList;
using BookList.Helpers;
using BookList.Model;
using BookList.Repository.ListRepository;
using BookList.Repository.UserRepository;
using BookList.Service.BookService;
using BookList.Service.ListService;
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

builder.Services.AddTransient<IListRepository, ListRepository>();
builder.Services.AddTransient<IListService, ListService>();

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
    Summary = "Catálogo da página inicial",
    Description = "Endpoint responsável por trazer o catálogo completo de livros para a página inicial"
});

app.MapGet("/catalog/{filter}", async ([FromServices] IBookService service, string filter) =>
    await service.FilterBooks(filter))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtro da página inicial",
    Description = "Endpoint responsável por filtrar os livros mostrados na página inicial",
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
    Description = "Endpoint responsável por trazer as informações do livro selecionado para a página de obra"
});

app.MapPost("/add", ([FromServices] IBookService service, Book book) =>
{
    service.AddBook(book);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddBook",
    Summary = "Adiciona livro",
    Description = "Endpoint responsável por adicionar um novo livro no banco de dados"
});

app.MapPost("/register", ([FromServices] IUserService service, RegisterUser user) =>
{
    return service.Register(user);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Register",
    Summary = "Registro",
    Description = "Endpoint responsável por adicionar um novo usuário no banco de dados"
});

app.MapPost("/login", ([FromServices] IUserService service, LoginUser user) =>
{
    return service.Login(user);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Login",
    Summary = "Login",
    Description = "Endpoint responsável por gerar um token para o usuário realizando login"
});

app.MapGet("/user", ([FromServices] IUserService service) =>
    service.User())
.WithOpenApi(operation => new(operation)
{
    OperationId = "User",
    Summary = "Usuário",
    Description = "Endpoint responsável por trazer as informações de um usuário logado"
});

app.MapGet("/profile/{username}", ([FromServices] IUserService service, string username) =>
    service.GetByUsername(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetByUsername",
    Summary = "Seleciona usuário",
    Description = "Endpoint responsável por trazer as informações do usuário selecionado para a página de perfil de usuário"
});

app.MapPost("/logout", ([FromServices] IUserService service) =>
{
    return service.Logout();
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Logout",
    Summary = "Logout",
    Description = "Endpoint responsável por deslogar o usuário da sessão"
});

app.MapPost("/addToList", ([FromServices] IListService service, [FromBody] ListEntry listEntry) =>
{
    return service.AddToList(listEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddToList",
    Summary = "Adiciona livro à lista",
    Description = "Endpoint responsável adicionar um livro à uma lista de leituras"
});

app.MapGet("/lists/{username}", ([FromServices] IListService service, string username) =>
    service.GetUserLists(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetUserLists",
    Summary = "Seleciona usuário",
    Description = "Endpoint responsável por trazer as leituras do usuário selecionado para a página de perfil de usuário"
});
#endregion

app.Run();