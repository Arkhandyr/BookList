using BookList.Model;

namespace BookList.Repository.ListRepository
{
    public class ListRepository : IListRepository
    {
        private readonly MongoDbContext context;

        public ListRepository(MongoDbContext context)
        {
            this.context = context;
        }

        public void AddToList(Users_Books users_Books)
        {
            context.Users_Books.InsertOne(users_Books);
        }
    }
}
