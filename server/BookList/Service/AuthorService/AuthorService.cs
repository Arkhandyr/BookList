using BookList.Helpers;
using BookList.Model;
using BookList.Repository.UserRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Bson;
using BookList.Service.AuthorService;
using BookList.Repository.AuthorRepository;

namespace BookList.Service.UserService
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IResult GetByName(string name)
        {
            Author author = _authorRepo.GetByName(name);

            if (author == null)
                return Results.BadRequest();

            return Results.Ok(author);
        }

        public IResult GetById(string id)
        {
            Author author = _authorRepo.GetById(id);

            if (author == null)
                return Results.BadRequest();

            return Results.Ok(author);
        }
    }
}
