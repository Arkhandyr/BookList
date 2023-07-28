﻿using MongoDB.Bson.Serialization.Attributes;

namespace BookList.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Picture { get; set; }
    }
}
