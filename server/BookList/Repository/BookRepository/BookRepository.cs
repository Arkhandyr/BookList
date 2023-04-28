﻿using MongoDB.Bson;
using MongoDB.Driver;

namespace DesafioTecfy
{
    public class BookRepository : IBookRepository
    {
        private readonly MongoDbContext context;

        public BookRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await context.Books.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Book>> FilterBooks(string query)
        {
            return await context.Books.Find(x => x.Title.ToLower().Contains(query) || x.Author.ToLower().Contains(query)).ToListAsync();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await context.Books.Find(x => x._id == id).FirstOrDefaultAsync();
        }

        public void AddBook(Book book)
        {
            context.Books.InsertOne(book);
        }

        public void UpdateBook(Book book)
        {
            context.Books.ReplaceOne(x => x._id == book._id, book);
        }

        public void DeleteBook(Guid id)
        {
            context.Books.DeleteOne(x => x._id == id);
        }
    }
}
