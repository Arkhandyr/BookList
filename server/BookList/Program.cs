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

app.MapGet("/catalog/trending/{page}", async ([FromServices] IBookService service, int page) => 
    await service.GetTrendingBooks(page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "Trending",
    Summary = "Catálogo 'em alta' da página inicial",
    Description = "Endpoint responsável por trazer o catálogo 'em alta' para a página inicial"
});

app.MapGet("/catalog/top/{page}", async ([FromServices] IBookService service, int page) =>
    await service.GetTopBooks(page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "Top 100",
    Summary = "Catálogo 'top 100' da página inicial",
    Description = "Endpoint responsável por trazer o catálogo 'top 100' para a página inicial"
});

app.MapGet("/booksByAuthor/{filter}", async ([FromServices] IBookService service, string filter) =>
    await service.FilterByAuthor(filter))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtro da página inicial",
    Description = "Endpoint responsável por filtrar os livros pelo autor"
});
#endregion

#region Book
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

app.MapPost("/addToList", ([FromServices] IListService service, [FromBody] ListEntry listEntry) =>
{
    return service.AddToList(listEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddToList",
    Summary = "Adiciona livro à lista",
    Description = "Endpoint responsável por adicionar um livro à uma lista de leituras"
});

app.MapPost("/removeFromList", ([FromServices] IListService service, [FromBody] ListEntry listEntry) =>
{
    return service.RemoveFromList(listEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "RemoveFromList",
    Summary = "Remove livro da lista",
    Description = "Endpoint responsável por remover um livro da lista de leituras"
});

app.MapGet("/books/{bookId}/{username}", ([FromServices] IListService service, string bookId, string username) =>
    service.GetBookStatus(bookId, username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetBookStatus",
    Summary = "Status de leitura do livro",
    Description = "Endpoint responsável por informar se o usuário já possui o livro em sua estante"
});

app.MapPost("/books/addReview", ([FromServices] IReviewService service, [FromBody] ReviewEntry reviewEntry) =>
{
    return service.AddReview(reviewEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "AddReview",
    Summary = "Adiciona resenha a um livro",
    Description = "Endpoint responsável por adicionar uma resenha a um livro"
});

app.MapPost("/books/deleteReview", ([FromServices] IReviewService service, [FromBody] ReviewEntry reviewEntry) =>
{
    return service.DeleteReview(reviewEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "DeleteReview",
    Summary = "Remove resenha de um livro",
    Description = "Endpoint responsável por remover uma resenha de um livro"
});

app.MapGet("/reviews/{bookId}", ([FromServices] IReviewService service, string bookId) =>
    service.GetBookReviews(bookId))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetBookReviews",
    Summary = "Resenhas da obra",
    Description = "Endpoint responsável por trazer todas as resenhas da obra na página da obra"
});

app.MapPost("/reviews/likeReview", ([FromServices] IReviewService service, [FromBody] LikeEntry likeEntry) =>
{
    return service.LikeReview(likeEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "LikeReview",
    Summary = "Curte uma resenha de livro",
    Description = "Endpoint responsável por marcar uma resenha como 'curtida'"
});

app.MapPost("/reviews/dislikeReview", ([FromServices] IReviewService service, [FromBody] LikeEntry likeEntry) =>
{
    return service.DislikeReview(likeEntry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "DislikeReview",
    Summary = "Remove curtida de uma resenha de livro",
    Description = "Endpoint responsável por desmarcar uma resenha como 'curtida'"
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

app.MapPost("/token", ([FromServices] IUserService service) =>
{
    return service.GetToken();
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetToken",
    Summary = "GetToken",
    Description = "Endpoint responsável por retornar o token de autenticação"
});
#endregion

#region Profile
app.MapGet("/profile/{username}", ([FromServices] IUserService service, string username) =>
    service.GetByUsername(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetByUsername",
    Summary = "Seleciona usuário",
    Description = "Endpoint responsável por trazer as informações do usuário selecionado para a página de perfil de usuário"
});

app.MapGet("/{list}/{username}/{page}", ([FromServices] IListService service, string list, string username, int page) =>
    service.GetUserList(username, list, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetUserList",
    Summary = "Estante virtual do usuário",
    Description = "Endpoint responsável por trazer as listas do usuário selecionado para a página de perfil"
});

app.MapGet("/badges/{username}", ([FromServices] IBadgeService service, string username) =>
    service.GetUserBadges(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetUserBadges",
    Summary = "Conquistas do usuário",
    Description = "Endpoint responsável por trazer as conquistas do usuário selecionado para a página de perfil de usuário"
});

app.MapGet("/bookCount/{username}", ([FromServices] IListService service, string username) =>
    service.CountBooks(username))
.WithOpenApi(operation => new(operation)
{
    OperationId = "BookCount",
    Summary = "Número de livros em cada lista",
    Description = "Endpoint responsável por trazer o número de livros que o usuário possui em cada lista"
});
#endregion

#region Author
app.MapGet("/author/{name}", ([FromServices] IAuthorService service, string name) =>
    service.GetByName(name))
.WithOpenApi(operation => new(operation)
{
    OperationId = "GetAuthorByName",
    Summary = "Seleciona autor",
    Description = "Endpoint responsável por trazer as informações do autor selecionado para a página de autor"
});

#endregion

#region Search
app.MapGet("/searchBooks/{filter}/{page}", async ([FromServices] IBookService service, string filter, int page) =>
    await service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterBooks",
    Summary = "Filtra livros",
    Description = "Endpoint responsável por filtrar os livros pelo nome"
});

app.MapGet("/searchAuthors/{filter}/{page}", ([FromServices] IAuthorService service, string filter, int page) =>
    service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterAuthor",
    Summary = "Filtra autores",
    Description = "Endpoint responsável por filtrar os autores pelo nome"
});

app.MapGet("/searchUsers/{filter}/{page}", ([FromServices] IUserService service, string filter, int page) =>
    service.FilterByName(filter, page))
.WithOpenApi(operation => new(operation)
{
    OperationId = "FilterUsers",
    Summary = "Filtra usuários",
    Description = "Endpoint responsável por filtrar os usuários pelo nome"
});
#endregion

#region Interaction
app.MapPost("/follow", ([FromServices] IInteractionService service, [FromBody] FollowEntry entry) =>
{
    return service.Follow(entry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Follow",
    Summary = "Segue um usuário",
    Description = "Endpoint responsável por seguir um usuário"
});

app.MapPost("/unfollow", ([FromServices] IInteractionService service, [FromBody] FollowEntry entry) =>
{
    return service.Unfollow(entry);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "Unfollow",
    Summary = "Deixa de seguir um usuário",
    Description = "Endpoint responsável por deixar de seguir um usuário"
});

app.MapGet("/followStatus/{user}/{user2}", ([FromServices] IInteractionService service, string user, string user2) =>
{
    return service.GetFollowStatus(user, user2);
})
.WithOpenApi(operation => new(operation)
{
    OperationId = "FollowStatus",
    Summary = "Verifica se um usuário segue outro usuário",
    Description = "Endpoint responsável por verificar o status de interação entre os usuários"
});
#endregion
#endregion
app.Run();