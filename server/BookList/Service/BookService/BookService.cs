﻿using BookList.Model;
using MongoDB.Bson;

namespace BookList.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Task<IEnumerable<Book>> GetAllBooks()
        {
            return _bookRepo.GetAllBooks();
        }

        public Task<IEnumerable<Book>> FilterBooks(string filter)
        {
            return _bookRepo.FilterBooks(filter);
        }

        public Task<Book> GetBookById(string id)
        {
            return _bookRepo.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _bookRepo.AddBook(book);
        }

        public void UpdateBook(Book book)
        {
            _bookRepo.UpdateBook(book);
        }

        public void DeleteBook(string id)
        {
            _bookRepo.DeleteBook(id);
        }
    }
}
