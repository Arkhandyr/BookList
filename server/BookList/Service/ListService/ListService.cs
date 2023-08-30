﻿using BookList.Helpers;
using BookList.Model;
using BookList.Repository.ListRepository;
using BookList.Repository.UserRepository;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace BookList.Service.ListService
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepo;
        private readonly IUserRepository _userRepo;

        public ListService(IUserRepository userRepo, IListRepository listRepo, IHttpContextAccessor contextAccessor)
        {
            _listRepo = listRepo;
            _userRepo = userRepo;
        }

        public IResult AddToList(ListEntry entry)
        {

            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = _userRepo.GetByUsername(entry.Username)._id,
                List = entry.ListName,
                Date = DateTime.Now
            };

            var response = _listRepo.UpsertEntry(userBook);

            //if (user == null)
            //    return Results.BadRequest();

            return Results.Ok(response);
        }

        public IResult RemoveFromList(ListEntry entry)
        {
            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(entry.BookId),
                User_id = _userRepo.GetByUsername(entry.Username)._id
            };

            var response = _listRepo.RemoveEntry(userBook);

            return Results.Ok(response);
        }

        public IResult GetUserLists(string username)
        {
            List<List<Book>> userLists = _listRepo.GetUserLists(username);

            return Results.Ok(userLists);
        }

        public IResult GetBookStatus(string bookId, string username)
        {
            Users_Books userBook = new()
            {
                Book_id = ObjectId.Parse(bookId),
                User_id = _userRepo.GetByUsername(username)._id
            };

            var bookStatus = _listRepo.GetBookStatus(userBook);

            return Results.Ok(bookStatus);
        }
    }
}
