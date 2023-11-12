﻿using BookList.Model;
using MongoDB.Bson;

namespace BookList.Repository.AuthorRepository
{
    public interface IAuthorRepository
    {
        public Author GetByName(string name);
        Task<IEnumerable<Author>> FilterByName(string query, int page);
    }
}
