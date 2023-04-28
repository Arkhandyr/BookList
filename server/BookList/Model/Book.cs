﻿using MongoDB.Bson.Serialization.Attributes;

namespace BookList.Model
{
    public class Book
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public Guid _id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Pages { get; set; }
        public string Cover { get; set; } = null!;
    }
}