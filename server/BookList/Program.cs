#region Configs
using BookList.Repository.BookRepository;
using BookList.Service.BookService;
using DesafioTecfy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddTransient<MongoDbContext>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBooksService, BooksService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().WithMethods("GET", "POST", "PUT", "DELETE"));
}
#endregion

#region Endpoints
app.MapGet("/catalog", async ([FromServices] IBooksService service) => await service.GetAllBooks()).WithName("Catalog");

app.MapGet("/book/{id}", async ([FromServices] IBooksService service, Guid id) => await service.GetBookById(id)).WithName("GetBookById");

app.MapGet("/catalog/{filter}", async ([FromServices] IBooksService service, string filter) => await service.FilterBooks(filter)).WithName("FilterBooks");

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
#endregion

app.Run();