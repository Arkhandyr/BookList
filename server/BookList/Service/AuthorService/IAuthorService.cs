namespace BookList.Service.AuthorService
{
    public interface IAuthorService
    {
        public IResult GetByName(string name);
        public IResult GetById(string id);
    }
}
