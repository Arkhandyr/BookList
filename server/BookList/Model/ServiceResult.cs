namespace BookList.Model
{
    public record ServiceResult<T>(bool Success, T Item) { }
}
