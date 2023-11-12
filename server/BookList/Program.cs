#region Config
using BookList;
using BookList.Helpers;
using BookList.Model;
using BookList.Repository.AuthorRepository;
using BookList.Repository.BadgeRepository;
using BookList.Repository.InteractionRepository;
using BookList.Repository.ListRepository;
using BookList.Repository.ReviewRepository;
using BookList.Repository.UserRepository;
using BookList.Service.AuthorService;
using BookList.Service.BadgeService;
using BookList.Service.BookService;
using BookList.Service.InteractionService;
using BookList.Service.ListService;
using BookList.Service.ReviewService;
using BookList.Service.UserService;
using Microsoft.AspNetCore.Mvc;

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

builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IReviewService, ReviewService>();

builder.Services.AddTransient<IBadgeRepository, BadgeRepository>();
builder.Services.AddTransient<IBadgeService, BadgeService>();

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

builder.Services.AddTransient<IInteractionRepository, InteractionRepository>();
builder.Services.AddTransient<IInteractionService, InteractionService>();

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
        .AllowAnyMethod());
}
else
{
    app.UseCors(x => x
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("https://booklist.fly.dev")
        .AllowAnyMethod());
}

var httpContext = app.Services.GetRequiredService<IHttpContextAccessor>().HttpContext;
#endregion

#region Endpoints
#region Main page

app.MapGet("/catalog/{page}", async ([FromServices] IBookService service, int page) => 
    await service.GetAllBooks(page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "Catalog",
    Summary = "Cat�logo da p�gina inicial",
    Description = "Endpoint respons�vel por trazer o cat�logo completo de livros para a p�gina inicial"
});

app.MapGet("/booksByAuthor/{filter}", async ([FromServices] IBookService service, string filter) =>
    await service.FilterByAuthor(filter))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtro da p�gina inicial",
    Description = "Endpoint respons�vel por filtrar os livros pelo autor"
});
#endregion

#region Book
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

app.MapPost("/addToList", ([FromServices] IListService service, [FromBody] ListEntry listEntry) =>
{
    return service.AddToList(listEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddToList",
    Summary = "Adiciona livro � lista",
    Description = "Endpoint respons�vel por adicionar um livro � uma lista de leituras"
});

app.MapPost("/removeFromList", ([FromServices] IListService service, [FromBody] ListEntry listEntry) =>
{
    return service.RemoveFromList(listEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "RemoveFromList",
    Summary = "Remove livro da lista",
    Description = "Endpoint respons�vel por remover um livro da lista de leituras"
});

app.MapGet("/books/{bookId}/{username}", ([FromServices] IListService service, string bookId, string username) =>
    service.GetBookStatus(bookId, username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetBookStatus",
    Summary = "Status de leitura do livro",
    Description = "Endpoint respons�vel por informar se o usu�rio j� possui o livro em sua estante"
});

app.MapPost("/books/addReview", ([FromServices] IReviewService service, [FromBody] ReviewEntry reviewEntry) =>
{
    return service.AddReview(reviewEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddReview",
    Summary = "Adiciona resenha a um livro",
    Description = "Endpoint respons�vel por adicionar uma resenha a um livro"
});

app.MapPost("/books/deleteReview", ([FromServices] IReviewService service, [FromBody] ReviewEntry reviewEntry) =>
{
    return service.DeleteReview(reviewEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "DeleteReview",
    Summary = "Remove resenha de um livro",
    Description = "Endpoint respons�vel por remover uma resenha de um livro"
});

app.MapGet("/reviews/{bookId}", ([FromServices] IReviewService service, string bookId) =>
    service.GetBookReviews(bookId))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetBookReviews",
    Summary = "Resenhas da obra",
    Description = "Endpoint respons�vel por trazer todas as resenhas da obra na p�gina da obra"
});

app.MapPost("/reviews/likeReview", ([FromServices] IReviewService service, [FromBody] LikeEntry likeEntry) =>
{
    return service.LikeReview(likeEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "LikeReview",
    Summary = "Curte uma resenha de livro",
    Description = "Endpoint respons�vel por marcar uma resenha como 'curtida'"
});

app.MapPost("/reviews/dislikeReview", ([FromServices] IReviewService service, [FromBody] LikeEntry likeEntry) =>
{
    return service.DislikeReview(likeEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "DislikeReview",
    Summary = "Remove curtida de uma resenha de livro",
    Description = "Endpoint respons�vel por desmarcar uma resenha como 'curtida'"
});
#endregion

#region Login
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

app.MapPost("/token", ([FromServices] IUserService service) =>
{
    return service.GetToken();
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetToken",
    Summary = "GetToken",
    Description = "Endpoint respons�vel por retornar o token de autentica��o"
});
#endregion

#region Profile
app.MapGet("/profile/{username}", ([FromServices] IUserService service, string username) =>
    service.GetByUsername(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetByUsername",
    Summary = "Seleciona usu�rio",
    Description = "Endpoint respons�vel por trazer as informa��es do usu�rio selecionado para a p�gina de perfil de usu�rio"
});

app.MapGet("/lists/{username}", ([FromServices] IListService service, string username) =>
    service.GetUserLists(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetUserLists",
    Summary = "Estante virtual do usu�rio",
    Description = "Endpoint respons�vel por trazer as leituras do usu�rio selecionado para a p�gina de perfil de usu�rio"
});

app.MapGet("/badges/{username}", ([FromServices] IBadgeService service, string username) =>
    service.GetUserBadges(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetUserBadges",
    Summary = "Conquistas do usu�rio",
    Description = "Endpoint respons�vel por trazer as conquistas do usu�rio selecionado para a p�gina de perfil de usu�rio"
});
#endregion

#region Author
app.MapGet("/author/{name}", ([FromServices] IAuthorService service, string name) =>
    service.GetByName(name))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetAuthorByName",
    Summary = "Seleciona autor",
    Description = "Endpoint respons�vel por trazer as informa��es do autor selecionado para a p�gina de autor"
});

#endregion

#region Search
app.MapGet("/searchBooks/{filter}/{page}", async ([FromServices] IBookService service, string filter, int page) =>
    await service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtra livros",
    Description = "Endpoint respons�vel por filtrar os livros pelo nome"
});

app.MapGet("/searchAuthors/{filter}/{page}", ([FromServices] IAuthorService service, string filter, int page) =>
    service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterAuthor",
    Summary = "Filtra autores",
    Description = "Endpoint respons�vel por por filtrar os autores pelo nome"
});

app.MapGet("/searchUsers/{filter}/{page}", ([FromServices] IUserService service, string filter, int page) =>
    service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterUsers",
    Summary = "Filtra usu�rios",
    Description = "Endpoint respons�vel por por filtrar os usu�rios pelo nome"
});
#endregion

#region Interaction
app.MapGet("/follow", ([FromServices] IInteractionService service, [FromBody] FollowEntry entry) =>
{
    return service.Follow(entry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterUsers",
    Summary = "Filtra usu�rios",
    Description = "Endpoint respons�vel por por filtrar os usu�rios pelo nome"
});

app.MapPost("/unfollow", ([FromServices] IInteractionService service, [FromBody] FollowEntry entry) =>
{
    return service.Unfollow(entry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterUsers",
    Summary = "Filtra usu�rios",
    Description = "Endpoint respons�vel por por filtrar os usu�rios pelo nome"
});
#endregion
#endregion
app.Run();